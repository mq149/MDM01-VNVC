using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyValueVaccinePriceListController : ControllerBase
    {
        private readonly IOptions<KeyValueDatabaseSettings> settings;

        private readonly IDatabase db;

        public KeyValueVaccinePriceListController(IOptions<KeyValueDatabaseSettings> settings)
        {
            this.settings = settings;
            Console.WriteLine(settings.Value.Host);
            Console.WriteLine(settings.Value.Port);
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(settings.Value.Host);
            db = redis.GetDatabase();
        }

        [HttpGet]
        public IEnumerable<Vaccine> GetAll()
        {
            var completeSet = db.HashGetAll("vaccines");

            if (completeSet.Length > 0)
            {
                var vaccines = Array.ConvertAll(completeSet, val =>
                    JsonSerializer.Deserialize<Vaccine>(val.Value)).ToList();
                return vaccines;
            }
            return null;
        }

        [HttpPost]
        public Vaccine CreateVaccine(Vaccine vac)
        {
            if (vac == null)
            {
                throw new ArgumentOutOfRangeException(nameof(vac));
            }

            var serializedVac = JsonSerializer.Serialize(vac);

            db.HashSet($"vaccines", new HashEntry[]
                {new HashEntry(vac.Id, serializedVac)});
            return vac;
        }

    }
}
