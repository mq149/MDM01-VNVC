using System;
using System.Text;
using MongoDB.Bson;
using Neo4j.Driver;

namespace MDM01_VNVC.Models
{
    public class Vaccine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProtectAgainst { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryOfOrigin { get; set; } = string.Empty;
        public double RetailPrice { get; set; } = 0;
        public double PreOrderPrice { get; set; } = 0;
        public string Status { get; set; } = string.Empty;

        public Vaccine()
        {
            
        }

        public Vaccine(string ProtectAgainst, string Name, string CountryOfOrigin, double RetailPrice, double PreOrderPrice, string Status)
        {
            this.ProtectAgainst = ProtectAgainst;
            this.Name = Name;
            this.CountryOfOrigin = CountryOfOrigin;
            this.RetailPrice = RetailPrice;
            this.PreOrderPrice = PreOrderPrice;
            this.Status = Status;
        }

        public Vaccine(INode node) {
            Id = node["Id"].As<string>();
            ProtectAgainst = node["ProtectAgainst"].As<string>();
            Name = node["Name"].As<string>();
            CountryOfOrigin = node["CountryOfOrigin"].As<string>();
            RetailPrice = node["RetailPrice"].As<double>();
            PreOrderPrice = node["PreOrderPrice"].As<double>();
            Status = node["Status"].As<string>();
        }

        public BsonDocument ShortenedBsonDocument() {
            return new BsonDocument()
            {
                { "id", Id },
                { "name", Name },
                { "price", RetailPrice }
            };
        }
    }
}
