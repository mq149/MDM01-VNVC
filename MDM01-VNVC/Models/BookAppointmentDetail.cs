using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models
{
    public class BookAppointmentDetail
    {
        public int Id_Item { get; set; }
        public string Name { get; set; }
        public BookAppointmentDetail(int Id, string Name)
        {
            this.Id_Item = Id;
            this.Name = Name;
        }
    }

}
