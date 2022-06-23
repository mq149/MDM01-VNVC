using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.MongoDB
{
    public class Customer
    {
        [BsonElement("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("customerCode")]
        public string CustomerCode { get; set; } = GetCustomerCode();
        [BsonElement("fullName")]
        public string FullName { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("address")]
        public Address Address { get; set; }

        public Customer()
        {
        }

        public Customer(BsonDocument document)
        {
            Id = document.GetValue("_id").ToString();
            CustomerCode = document.GetValue("customerCode").ToString();
            FullName = document.GetValue("fullName").ToString();
            Gender = document.GetValue("gender").ToString();
            DateOfBirth = document.GetValue("dateOfBirth").ToLocalTime();
            PhoneNumber = document.GetValue("phoneNumber").ToString();
            Email = document.GetValue("email").ToString();
            Address = new Address(document.GetValue("address").ToBsonDocument());
        }

        public BsonDocument Shortened()
        {
            return new BsonDocument {
                { "customerCode", CustomerCode },
                { "fullName", FullName },
                { "phoneNumber", PhoneNumber },
                { "dateOfBirth", DateOfBirth }
            };
        }

        private static string GetCustomerCode()
        {
            var ticks = new DateTime(2022, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");
            return uniqueId;
        }
    }
}
