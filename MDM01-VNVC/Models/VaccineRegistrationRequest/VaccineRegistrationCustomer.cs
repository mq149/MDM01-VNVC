using System;
using System.Collections.Generic;
using System.Linq;
using MDM01_VNVC.Models.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.VaccineRegistrationRequest
{
    public class VaccineRegistrationCustomer
    {
        [BsonElement("information")]
        public Customer Information { get; set; }
        [BsonElement("relationship")]
        public string Relationship { get; set; }
        [BsonElement("vaccines")]
        public List<Vaccine> Vaccines { get; set; }
        [BsonElement("vnvcCenter")]
        public VnvcCenter VnvcCenter { get; set; }
        [BsonElement("subTotal")]
        public double SubTotal { get; set; }

        public VaccineRegistrationCustomer(Customer information,
            string relationship, List<Vaccine> vaccines,
            VnvcCenter vnvcCenter, double subTotal)
        {
            Information = information;
            Relationship = relationship;
            Vaccines = vaccines;
            VnvcCenter = vnvcCenter;
            SubTotal = subTotal;
        }

        public BsonDocument ShortenedBsonDocument()
        {
            var bsonVaccineArray = new BsonArray();
            Vaccines.ForEach(v => bsonVaccineArray.Add(v.ShortenedBsonDocument()));
            return new BsonDocument()
            {
                { "information", Information.Shortened() },
                { "relationship", Relationship },
                { "vaccines", bsonVaccineArray },
                { "vnvcCenter", VnvcCenter.ToBsonDocument() },
                { "subTotal", SubTotal }
            };
        }
    }
}
