using System;
using System.Collections.Generic;
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
    [Route("payment-methods")]
    public class PaymentMethodController
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly IMongoDatabase database;

        public PaymentMethodController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
            BsonClassMap.RegisterClassMap<PaymentMethod>();
        }

        [HttpGet]
        public List<PaymentMethod> GetAll()
        {
            List<PaymentMethod> relationships = new List<PaymentMethod>();
            var collection = database.GetCollection<BsonDocument>(settings.Value.PaymentMethodCollectionName);
            var documents = collection.Find(new BsonDocument()).ToList();
            foreach (var document in documents)
            {
                // TODO: Fix key not found case
                relationships.Add(new PaymentMethod(document.GetValue("id").ToString(),
                    document.GetValue("name").ToString()));
            }
            return relationships;
        }
    }
}
