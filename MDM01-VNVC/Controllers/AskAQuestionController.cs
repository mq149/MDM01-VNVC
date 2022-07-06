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
    public class AskAQuestionController : ControllerBase
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<AskAQuestion> collection;
        public AskAQuestionController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            dbClient = new MongoClient(settings.Value.ConnectionString);
            collection = dbClient.GetDatabase(settings.Value.DatabaseName).GetCollection<AskAQuestion>(settings.Value.AskAQuestionCollectionName);
        }

        [HttpGet]
        public IEnumerable<AskAQuestion> Get()
        {
            List<AskAQuestion> ask = collection.Find(s => s.Question != null).ToList();
            return ask;
        }

        [HttpPost]
        public JsonResult NewQuestion(AskAQuestion ask)
        {
            
            try
            {
                collection.InsertOne(ask);
            }
            catch (Exception)
            {
                return new JsonResult("Failed");
            }              
            return new JsonResult("Success!");
        }
    }
}
