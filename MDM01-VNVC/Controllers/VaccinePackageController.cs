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
                    "match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC{Id:"+id+"})-[r:Gom]->(vc:Vaccine) return vc,d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi,r.soLuong as soluong");
                if (result.FirstOrDefault()["tendm"].As<string>() != null)
                {
                    packagevaccine.Id = id;
                    packagevaccine.DanhMuc = result.FirstOrDefault()["tendm"].As<string>();
                    packagevaccine.GoiVaccine = result.FirstOrDefault()["tengoi"].As<string>();
                    packagevaccine.LoaiGoi = result.FirstOrDefault()["loaigoi"].As<string>();
                    List<VacinePackageDescribe> vacinepackagedescribes = new List<VacinePackageDescribe>();
                    foreach (var r in result)
                    {
                        var node = r["vc"].As<INode>();
                        VacinePackageDescribe t = new VacinePackageDescribe(new Vaccine(node), r["soluong"].As<int>());
                        vacinepackagedescribes.Add(t);
                    }
                    packagevaccine.VacinePackageDescribes = vacinepackagedescribes;
                    packagevaccine.TotalPrice = vacinepackagedescribes.Sum(x => x.Vaccine.RetailPrice * x.Quantity);
                    packagevaccine.TotalCount = vacinepackagedescribes.Sum(x => x.Quantity);
                }
            }
            _driver.CloseAsync();
            return packagevaccine;
        }
    }
}
