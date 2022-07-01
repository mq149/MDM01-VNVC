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
    public class BookAppointmentController : ControllerBase
    {
        private readonly IOptions<DocumentDatabaseSettings> settings;
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<BookAppointment> collection;
        public BookAppointmentController(IOptions<DocumentDatabaseSettings> settings)
        {
            this.settings = settings;
            dbClient = new MongoClient(settings.Value.ConnectionString);
            collection=dbClient.GetDatabase(settings.Value.DatabaseName).GetCollection<BookAppointment>(settings.Value.BookAppointmentsCollectionName);
        }  
        
        [HttpGet]
        public JsonResult GetAll()
        {
            var dbList = collection.AsQueryable().ToList();
            return new JsonResult(dbList);
        }

        [HttpGet("{value}")]
        public JsonResult Find(string value)
        {
            var filter1 = Builders<BookAppointment>.Filter.Eq("FullName",value);
            var filter2 = Builders<BookAppointment>.Filter.Eq("PhoneNumber", value);
            var dbList1 = collection.Find(filter1).ToList();
            var dbList2 = collection.Find(filter2).ToList();
            if (dbList2.Count() == 0) return new JsonResult(0);
            else
                return new JsonResult(dbList2[dbList2.Count()-1]);
            
        }

        [HttpPost]
        public JsonResult NewAppoint(BookAppointment book)
        {
            book.Id= collection.AsQueryable().Count()+1;
            try
            {
                collection.InsertOne(book);
            }
            catch (Exception)
            {
                return new JsonResult("Failed");
            }              
            return new JsonResult("Success!");
        }
    }
}