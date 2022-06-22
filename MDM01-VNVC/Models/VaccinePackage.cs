using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MDM01_VNVC.Models;

namespace MDM01_VNVC.Models
{
    
    public class VacinePackage
    {
        public int Id { get; set; }
        //public string Description { get; set; }
        public string DanhMuc { get; set; }
        public string GoiVaccine { get; set; }
        public string LoaiGoi { get; set; }
        public double TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public string Describe { get; set; }
        public List<VacinePackageDescribe> VacinePackageDescribes { get; set; }
    }

    

}