using System;
namespace MDM01_VNVC.Models
{
    public class GraphDatabaseSettings
    {
        public string URI { get; set; } = null!;

        public string User { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }
}
