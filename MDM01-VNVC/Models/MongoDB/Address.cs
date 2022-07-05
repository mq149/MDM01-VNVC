using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.MongoDB
{
    public class Address
    {
        [BsonElement("addressLine")]
        public string AddressLine { get; set; }
        [BsonElement("ward")]
        public string Ward { get; set; }
        [BsonElement("district")]
        public string District { get; set; }
        [BsonElement("province")]
        public string Province { get; set; }

        public Address() { }

        public Address(string addressLine, string ward, string district, string province)
        {
            AddressLine = addressLine;
            Ward = ward;
            District = district;
            Province = province;
        }

        public Address(BsonDocument document)
        {
            AddressLine = document.GetValue("addressLine").ToString();
            Ward = document.GetValue("ward").ToString();
            District = document.GetValue("district").ToString();
            Province = document.GetValue("province").ToString();
        }
    }
}
