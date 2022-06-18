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
    public class VaccinePackageController : ControllerBase, IDisposable
    {
        private readonly IOptions<GraphDatabaseSettings> settings;
        private readonly IDriver _driver;
        private bool _disposed = false;
        private Action<SessionConfigBuilder> sessionConfig;

        public VaccinePackageController(IOptions<GraphDatabaseSettings> settings)
        {
            this.settings = settings;
            _driver = GraphDatabase.Driver(settings.Value.URI,
                AuthTokens.Basic(settings.Value.User, settings.Value.Password));
            sessionConfig = SessionConfigBuilder.ForDatabase("vnvc");
        }

        ~VaccinePackageController() => Dispose(false);

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

        [HttpGet("{id}")]
        public VacinePackage GetPackage(int id)
        {
            VacinePackage packagevaccine = new VacinePackage();
            using (var session = _driver.Session(sessionConfig))
            {
                var result = session.Run(
                    "match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC{Id:"+id+"})-[r:Gom]->(vc:Vaccine) return d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi,sum(vc.RetailPrice) as price,vc");
                packagevaccine.tenDM = result.FirstOrDefault()["tendm"].As<string>();
                packagevaccine.tenGoi = result.FirstOrDefault()["tengoi"].As<string>();
                packagevaccine.loaiGoi = result.FirstOrDefault()["loaigoi"].As<string>();
                packagevaccine.price = result.FirstOrDefault()["price"].As<decimal>();
                foreach (var r in result)
                {
                    var node = r["vc"].As<INode>();
                    packagevaccine.Vaccines.Add(new Vaccine(node));
                }
            }
            _driver.CloseAsync();
            return packagevaccine;
        }
    }
}
