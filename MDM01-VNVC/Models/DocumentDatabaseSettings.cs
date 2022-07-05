using System;
namespace MDM01_VNVC.Models
{
    public class DocumentDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
        
        public string VaccinesCollectionName { get; set; } = null!;

        public string CustomerRelationshipCollectionName { get; set; } = null!;

        public string PaymentMethodCollectionName { get; set; } = null!;

        public string CustomerCollectionName { get; set; } = null!;

        public string VaccineRegistrationCollectionName { get; set; } = null!;
        
        public string BookAppointmentsCollectionName { get; set; } = null!;
    }
}