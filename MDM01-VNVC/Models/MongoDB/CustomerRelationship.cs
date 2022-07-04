using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.MongoDB
{
    public class CustomerRelationship
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        public CustomerRelationship()
        {
            
        }

        [BsonConstructor]
        public CustomerRelationship(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
