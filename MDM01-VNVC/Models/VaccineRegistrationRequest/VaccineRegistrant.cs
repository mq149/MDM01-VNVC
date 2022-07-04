using System;
using MDM01_VNVC.Models.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models.VaccineRegistrationRequest
{
    public class VaccineRegistrant
    {
        [BsonElement("information")]
        public Customer Information { get; set; }
        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }

        public VaccineRegistrant(Customer information, string paymentMethod)
        {
            Information = information;
            PaymentMethod = paymentMethod;
        }
    }
}
