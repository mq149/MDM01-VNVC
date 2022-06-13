using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models
{
    public class BookAppointment
    {
        public ObjectId Id { get; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex {get;set;}
        public string CusID {get;set;}
        public string City {get;set;}
        public string District {get;set;}
        public string Ward {get;set;}
        public string Street {get;set;}
        public string NameContact {get;set;}
        public string ContractType {get;set;}
        public string PhoneNumber { get; set; }
        public string VaccineID { get; set; } 
        public string Center {get;set;}
        public DateTime AppointmentDate { get; set;}

        public BookAppointment(string fullName, DateTime birthDate, string sex, string cusid,
        string city, string district, string ward, string street, string namecontract,
        string contractype, string phonenumber, string vaccineid, string center, DateTime appointmentdate)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Sex=sex;
            CusID=cusid;
            City=city;
            District=district;
            Ward=ward;
            Street=street;
            NameContact=namecontract;
            ContractType=contractype;
            PhoneNumber= phonenumber;
            VaccineID= vaccineid;
            Center=center;
            AppointmentDate=appointmentdate;    
        } 
    }

}