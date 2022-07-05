using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Neo4j.Driver;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MDM01_VNVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InjectionCenterController : ControllerBase
    {
        private readonly ILogger<InjectionCenterController> _logger;
        private readonly IDriver _driver;
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private Action<SessionConfigBuilder> sessionConfig;

        public InjectionCenterController(ILogger<InjectionCenterController> logger, IOptions<DocumentDatabaseSettings> settings)
        {
            _logger = logger;
            this.settings = settings;
        }

        [HttpGet]
        public IEnumerable<InjectionCenter_Tinh> GetDistrict()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("vnvc");
            var collection = database.GetCollection<InjectionCenter_Tinh>("injectioncenter");
            List<InjectionCenter_Tinh> centers = collection.Find(s => s.Tinh != null).ToList();
            return centers;
        }

        [HttpGet("{key}")]
        public IEnumerable<InjectionCenter_Tinh> GetCenterForDistrict(string key)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("vnvc");
            var collection = database.GetCollection<InjectionCenter_Tinh>("injectioncenter");
            List<InjectionCenter_Tinh> districts = collection.Find(s => s.Tinh == key).ToList();
            return districts;
        }
    }
}
