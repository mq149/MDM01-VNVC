using System;
using System.Threading.Tasks;
using MDM01_VNVC.Models;
using MDM01_VNVC.Models.MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly IMongoDatabase database;

        public CustomerController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
        }

        [HttpGet("")]
        public IActionResult One(string customerCode, DateTime dateOfBirth)
        {
            var collection = database.GetCollection<BsonDocument>(settings.Value.CustomerCollectionName);
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("customerCode", customerCode)
                & filterBuilder.Eq("dateOfBirth", dateOfBirth);
            var customerDoc = collection.Find(filter).FirstOrDefault();
            if (customerDoc != null)
            {
                var customer = new Customer(customerDoc);
                return new OkObjectResult(customer);
            }
            return new NotFoundResult();
        }
    }
}
