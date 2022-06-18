using System;
using System.Collections.Generic;
using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neo4j.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VaccinePriceListController : ControllerBase, IDisposable
    {
        private readonly IOptions<GraphDatabaseSettings> settings;
        private readonly IDriver _driver;
        private bool _disposed = false;
        private Action<SessionConfigBuilder> sessionConfig;

        public VaccinePriceListController(IOptions<GraphDatabaseSettings> settings)
        {
            this.settings = settings;
            Console.WriteLine(settings.Value.URI);
            Console.WriteLine(settings.Value.User);
            Console.WriteLine(settings.Value.Password);
            _driver = GraphDatabase.Driver(settings.Value.URI,
                AuthTokens.Basic(settings.Value.User, settings.Value.Password));
            sessionConfig = SessionConfigBuilder.ForDatabase("vnvc");
        }

        ~VaccinePriceListController() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _driver?.Dispose();
            }
            _disposed = true;
        }

        [HttpGet]
        public List<Vaccine> GetAll()
        {
            List<Vaccine> vaccines = new List<Vaccine>();
            using (var session = _driver.Session(sessionConfig))
            {

                var result = session.Run(
                    "MATCH (v:Vaccine)" +
                    "RETURN v");
                
                foreach (var r in result)
                {
                    var node = r["v"].As<INode>();
                    vaccines.Add(new Vaccine(node));
                }
            }
            _driver.CloseAsync();
            return vaccines;
        }
    }
}
