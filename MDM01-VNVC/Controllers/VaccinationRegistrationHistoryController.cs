using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Neo4j.Driver;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MDM01_VNVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VaccinationRegistrationHistoryController : ControllerBase
    {
        private readonly ILogger<VaccinationRegistrationHistoryController> _logger;
        private readonly IDriver _driver;
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private Action<SessionConfigBuilder> sessionConfig;

        public VaccinationRegistrationHistoryController(ILogger<VaccinationRegistrationHistoryController> logger, IOptions<DocumentDatabaseSettings> settings)
        {
            _logger = logger;
            this.settings = settings;
         }

        [HttpGet]
        public IEnumerable<VaccinationRegistrationHistory> Get()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("vnvc");
            var collection = database.GetCollection<VaccinationRegistrationHistory>("vaccines");
            List<VaccinationRegistrationHistory> historys = collection.Find(s => s.NguoiLienHe_HoTen != null).ToList();
            return historys;
        }


        [HttpGet("{sdt}")]
        public IEnumerable<VaccinationRegistrationHistory> SearchForPhoneNumber(string sdt)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("vnvc");
            var collection = database.GetCollection<VaccinationRegistrationHistory>("vaccines");

            List<VaccinationRegistrationHistory> historys = collection.Find(s => s.NguoiTiem_SDT == sdt).ToList();
            return historys;
        }
    }
}
