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
    public class InjectionCenter
    {
        //[BsonId]
        //public ObjectId Id { get; set; }

        [BsonElement("TenTT")]
        public string TenTT { get; set; }

        [BsonElement("DiaChi")]
        public string DiaChi { get; set; }

        public InjectionCenter(string TenTT, string DiaChi)
        {
            this.TenTT = TenTT;
            this.DiaChi = DiaChi;
        }
    }
}
