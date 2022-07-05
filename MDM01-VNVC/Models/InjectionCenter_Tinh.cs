using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDM01_VNVC.Models
{
    public class InjectionCenter_Tinh
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Tinh")]
        public string Tinh { get; set; }

        [BsonElement("TrungTam")]
        public List<InjectionCenter> TrungTam { get; set; }

        public InjectionCenter_Tinh(string Tinh, List<InjectionCenter> TrungTam)
        {
            this.Tinh = Tinh;
            this.TrungTam = TrungTam;
        }
    }
}
