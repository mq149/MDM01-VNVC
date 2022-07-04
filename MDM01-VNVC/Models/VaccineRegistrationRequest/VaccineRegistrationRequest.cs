using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.VaccineRegistrationRequest
{
    public class VaccineRegistrationRequest
    {
        [BsonElement("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("registrant")]
        public VaccineRegistrant Registrant { get; set; }
        [BsonElement("customers")]
        public List<VaccineRegistrationCustomer> Customers { get; set; }
        [BsonElement("total")]
        public double Total { get; set; }

        public VaccineRegistrationRequest(
            VaccineRegistrant registrant,
            List<VaccineRegistrationCustomer> customers,
            double total)
        {
            Registrant = registrant;
            Customers = customers;
            Total = total;
        }

        public BsonDocument ToBsonDocument()
        {
            var bsonCustomerArray = new BsonArray();
            Customers.ForEach(c => bsonCustomerArray.Add(c.ShortenedBsonDocument()));
            return new BsonDocument()
            {
                { "registrant", new BsonDocument {
                        { "information", Registrant.Information.Shortened() },
                        { "payment_method", Registrant.PaymentMethod }
                }},
                { "customers", bsonCustomerArray },
                { "total", Total }
            };
        }
    }
}
