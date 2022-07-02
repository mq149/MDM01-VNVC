using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MDM01_VNVC.Models;

namespace MDM01_VNVC.Models
{
   public class VacinePackageDescribe
   {
        public Vaccine Vaccine { get; set; }
        public int Quantity { get; set; }
        public VacinePackageDescribe (Vaccine Vaccine,int Quantity)
        {
            this.Vaccine = Vaccine;
            this.Quantity = Quantity;
        }
    }
}

