using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models
{
    public class VacinePackage
    {
        public int Id { get; set; }
        //public string Description { get; set; }
        public string DanhMuc { get; set; }
        public string GoiVaccine { get; set; }
        public string LoaiGoi { get; set; }
        public decimal Price { get; set; }
        List<Vacines> Vaccines { get; set; }
    }

}