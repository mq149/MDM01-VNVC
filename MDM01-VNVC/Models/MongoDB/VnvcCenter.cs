using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.MongoDB
{
    public class VnvcCenter
    {
        [BsonElement("province")]
        public string Province { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        public VnvcCenter(string province, string name)
        {
            Province = province;
            Name = name;
        }
    }
}
