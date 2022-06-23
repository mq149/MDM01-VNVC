using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MDM01_VNVC.Models;
using MDM01_VNVC.Models.MongoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("customer-relationships")]
    public class CustomerRelationshipController
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly IMongoDatabase database;

        public CustomerRelationshipController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
            BsonClassMap.RegisterClassMap<CustomerRelationship>();
        }

        [HttpGet]
        public List<CustomerRelationship> GetAll()
        {
            List<CustomerRelationship> relationships = new List<CustomerRelationship>();
            var collection = database.GetCollection<BsonDocument>(settings.Value.CustomerRelationshipCollectionName);
            var documents = collection.Find(new BsonDocument()).ToList();
            foreach (var document in documents)
            {
                // TODO: Fix key not found case
                relationships.Add(new CustomerRelationship(document.GetValue("id").ToString(),
                    document.GetValue("name").ToString()));
            }
            return relationships;
        }
    }
}
