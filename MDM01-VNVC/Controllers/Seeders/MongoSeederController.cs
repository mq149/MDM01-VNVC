using System;
using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("seeder/mongo/")]
    public class MongoSeederController
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly IMongoDatabase database;

        public MongoSeederController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
        }

        [HttpGet("customer-relationships")]
        public string SeedCustomerRelationships()
        {
            var collection = database.GetCollection<BsonDocument>(settings.Value.CustomerRelationshipCollectionName);
            var count = collection.CountDocuments(new BsonDocument());
            if (count > 0)
            {
                return "Collection " + settings.Value.CustomerRelationshipCollectionName + " currently has " + count.ToString() + " documents.";
            }
            var documents = new BsonDocument[] {
                new BsonDocument { { "id", "1" }, { "name", "Bản thân" } },
                new BsonDocument { { "id", "2" }, { "name", "Cha" } },
                new BsonDocument { { "id", "3" }, { "name", "Mẹ" } },
                new BsonDocument { { "id", "4" }, { "name", "Ông" } },
                new BsonDocument { { "id", "5" }, { "name", "Bà" } },
                new BsonDocument { { "id", "6" }, { "name", "Anh" } },
                new BsonDocument { { "id", "7" }, { "name", "Chị" } },
                new BsonDocument { { "id", "8" }, { "name", "Em" } },
                new BsonDocument { { "id", "9" }, { "name", "Vợ" } },
                new BsonDocument { { "id", "10" }, { "name", "Chồng" } },
                new BsonDocument { { "id", "11" }, { "name", "Con" } },
                new BsonDocument { { "id", "12" }, { "name", "Cùng hộ khẩu" } },
            };
            collection.InsertMany(documents);
            return "Populated " + documents.Length.ToString() + " documents into collection " + settings.Value.CustomerRelationshipCollectionName + ".";
        }

        [HttpGet("payment-methods")]
        public string SeedPaymentMethods()
        {
            var collection = database.GetCollection<BsonDocument>(settings.Value.PaymentMethodCollectionName);
            var count = collection.CountDocuments(new BsonDocument());
            if (count > 0)
            {
                return "Collection " + settings.Value.PaymentMethodCollectionName + " currently has " + count.ToString() + " documents.";
            }
            var documents = new BsonDocument[] {
                new BsonDocument { { "id", "1" }, { "name", "Thanh toán bằng thẻ thanh toán nội địa (ATM)" } },
                new BsonDocument { { "id", "2" }, { "name", "Thanh toán bằng thẻ VISA/MASTER/JCB" } },
                new BsonDocument { { "id", "3" }, { "name", "Thanh toán bằng thẻ thành viên" } },
                new BsonDocument { { "id", "4" }, { "name", "Thanh toán qua chuyển khoản" } },
                new BsonDocument { { "id", "5" }, { "name", "Thanh toán tại trung tâm" } },
            };
            collection.InsertMany(documents);
            return "Populated " + documents.Length.ToString() + " documents into collection " + settings.Value.PaymentMethodCollectionName + ".";
        }
    }
}
