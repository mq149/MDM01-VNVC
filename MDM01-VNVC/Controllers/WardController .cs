using System;
using System.Collections.Generic;
using System.Linq;
using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neo4j.Driver;

namespace MDM01_VNVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WardController : ControllerBase, IDisposable
    {
        private readonly IOptions<GraphDatabaseSettings> settings;
        private readonly IDriver _driver;
        private bool _disposed = false;
        private Action<SessionConfigBuilder> sessionConfig;

        public WardController(IOptions<GraphDatabaseSettings> settings)
        {
            this.settings = settings;
            _driver = GraphDatabase.Driver(settings.Value.URI,
                AuthTokens.Basic(settings.Value.User, settings.Value.Password));
            sessionConfig = SessionConfigBuilder.ForDatabase("vnvc");
        }

        ~WardController() => Dispose(false);

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

        [HttpGet("{value}")]
        public List<string> Get(string value)
        {
            List<string> a = new List<string>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = "match (q:QuanHuyen {tenQuanHuyen:'" + value + "'})-[:Chua]->(p:PhuongXa) return p.tenPhuongXa as ward order by ward;";
                Console.WriteLine(statement);
                var result = session.Run(statement);
                
                foreach(var r in result)
                {
                    string i=r["ward"].As<string>();
                    a.Add(i);
                }
            }
            _driver.CloseAsync();
            return a;
        }
    }
}
