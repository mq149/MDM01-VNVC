using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MDM01_VNVC.Models;
using MDM01_VNVC.Models.MongoDB;
using MDM01_VNVC.Models.VaccineRegistrationRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Neo4j.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrepaidVaccineProgramController : ControllerBase
    {
        private readonly IOptions<GraphDatabaseSettings> graphDbSettings;
        private readonly IDriver neo4jDriver;
        private Action<SessionConfigBuilder> neo4jSessionConfig;
        private readonly IOptions<DocumentDatabaseSettings> documentDbSettings;
        private readonly MongoClient mongoClient;
            private readonly IMongoDatabase mongoDb;

        public PrepaidVaccineProgramController(
            IOptions<GraphDatabaseSettings> graphDbSettings,
            IOptions<DocumentDatabaseSettings> documentDbSettings)
        {
            this.graphDbSettings = graphDbSettings;
            neo4jDriver = GraphDatabase.Driver(graphDbSettings.Value.URI,
                AuthTokens.Basic(graphDbSettings.Value.User, graphDbSettings.Value.Password));
            neo4jSessionConfig = SessionConfigBuilder.ForDatabase(graphDbSettings.Value.DatabaseName);
            this.documentDbSettings = documentDbSettings;
            mongoClient = new MongoClient(documentDbSettings.Value.ConnectionString);
            mongoDb = mongoClient.GetDatabase(documentDbSettings.Value.DatabaseName);
        }

        [HttpPost("")]
        public VaccineRegistrationRequest New(VaccineRegistrationRequest request)
        {
            // Handle the registrant (if the account is existed, update, else add new)
            var registrant = request.Registrant;
            var updatedRegistrant = UpsertCustomer(registrant.Information);
            request.Registrant.Information = updatedRegistrant;

            // Handle the customers (if the information matches, update, else add new)
            // Get registered vaccine ids
            List<string> vaccineIds = new List<string>();
            for (var i = 0; i < request.Customers.Count; i++)
            {
                var updatedCustomer = UpsertCustomer(request.Customers[i].Information);
                request.Customers[i].Information = updatedCustomer;
                for (var j = 0; j < request.Customers[i].Vaccines.Count; j++)
                {
                    vaccineIds.Add(request.Customers[i].Vaccines[j].Id);
                }
            }

            Console.WriteLine("Registered " + vaccineIds.Count + " vaccines");

            // Check the total,...
            var total = CalculateTotal(vaccineIds);
            request.Total = total;

            // Handle the registration request
            CreateRegistration(request);

            // Execute all the code above inside a transaction
            //using (var session = mongoClient.StartSession())
            //{
            //    session.StartTransaction();
            //    try
            //    {

            //        session.CommitTransaction();
            //    } catch (Exception e)
            //    {
            //        Console.WriteLine("Error writing to MongoDB: " + e.Message);
            //        session.AbortTransaction();
            //    }
            //}

            return request;
        }


        // Private functions
        private Customer UpsertCustomer(Customer customer)
        {
            var collection = mongoDb.GetCollection<BsonDocument>(documentDbSettings.Value.CustomerCollectionName);
            var filterBuilder = Builders<BsonDocument>.Filter;
            var updateBuilder = Builders<BsonDocument>.Update;

            var filter = filterBuilder.Eq("fullName", customer.FullName)
                & filterBuilder.Eq("phoneNumber", customer.PhoneNumber);

            if (collection.Find(filter).ToList().Count > 0)
            {
                var update = updateBuilder.Set("address", customer.Address)
                    .Set("email", customer.Email);
                collection.UpdateOne(filter, update);
            } else {
                collection.InsertOne(customer.ToBsonDocument());
            }
            var updatedCustomer = collection.Find(filter).First();
            return new Customer(updatedCustomer);
        }

        private bool CreateRegistration(VaccineRegistrationRequest request)
        {
            var bsonRequest = request.ToBsonDocument();
            var collection = mongoDb.GetCollection<BsonDocument>(documentDbSettings.Value.VaccineRegistrationCollectionName);
            collection.InsertOne(bsonRequest);
            return true;
        }

        private double CalculateTotal(List<string> vaccineIds)
        {
            var vaccineIdStrings = vaccineIds.Select(id => "\"" + id + "\"");
            var filterString = "v.Id IN [" + String.Join(", ", vaccineIdStrings) + "]";
            var query = @"
                MATCH (v:Vaccine)
                WHERE " + filterString + @"
                RETURN SUM(v.RetailPrice) AS total
            ";
            using (var session = neo4jDriver.Session(neo4jSessionConfig))
            {
                var result = session.Run(query);
                foreach (var r in result)
                {
                    var total = r.As<IRecord>().Values["total"].As<double>();
                    return total;
                }
            }
            return 0;
        }
    }
}
