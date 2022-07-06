using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MDM01_VNVC.Models;
using Microsoft.Extensions.Options;
namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrequentQuestionsController : ControllerBase
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<FrequentQuestions> collection;
        public FrequentQuestionsController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            dbClient = new MongoClient(settings.Value.ConnectionString);
            collection = dbClient.GetDatabase(settings.Value.DatabaseName).GetCollection<FrequentQuestions>(settings.Value.FrequentQuestionsCollectionName);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var dbList = collection.AsQueryable().ToList();
            return new JsonResult(dbList);
        }
    }
}
