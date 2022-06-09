using System;
using System.Text;

namespace MDM01_VNVC.Models
{
    public class Vaccine
    {
        public string Id { get; set; } = $"vaccine:{Guid.NewGuid().ToString()}";
        public string ProtectAgainst { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryOfOrigin { get; set; } = string.Empty;
        public double RetailPrice { get; set; } = 0;
        public double PreOrderPrice { get; set; } = 0;
        public string Status { get; set; } = string.Empty;

        public Vaccine()
        {
            
        }

        public Vaccine(string protectAgainst, string name, string countryOfOrigin, double retailPrice, double preOrderPrice, string status)
        {
            ProtectAgainst = protectAgainst;
            Name = name;
            CountryOfOrigin = countryOfOrigin;
            RetailPrice = retailPrice;
            PreOrderPrice = preOrderPrice;
            Status = status;
        }
    }
}
