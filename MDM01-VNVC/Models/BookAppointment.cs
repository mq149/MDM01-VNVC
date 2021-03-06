using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models
{
    public class BookAppointment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Sex {get;set;}
        public string CusID {get;set;}
        public string City {get;set;}
        public string District {get;set;}
        public string Ward {get;set;}
        public string Street {get;set;}
        public string NameContact {get;set;}
        public string ContactType {get;set;}
        public string PhoneNumber { get; set; }
        public string VaccineType { get; set; }
        public List<BookAppointmentDetail> BookAppointmentDetail { get; set; } 
        public string Center {get;set;}
        public string AppointmentDate { get; set; }   
        public double TotalPrice { get; set; }
    }

}
