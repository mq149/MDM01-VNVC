using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.MongoDB
{
    public class PaymentMethod
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        public PaymentMethod()
        {

        }

        [BsonConstructor]
        public PaymentMethod(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
