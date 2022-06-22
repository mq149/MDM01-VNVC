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

        [HttpGet]
        public List<VacinePackage> GetAllPackage()
        {
            List<VacinePackage> packagevaccines = new List<VacinePackage>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = $"match (l:LoaiGoiVC) return l.Id as Id";
                var result = session.Run(statement);
                List<int> dsVc = new List<int>();
                foreach (var r in result)
                {
                    dsVc.Add(r["Id"].As<int>());
                }
                foreach (int i in dsVc)
                {
                    VacinePackage packagevaccine = new VacinePackage();
                    List<VacinePackageDescribe> vacinepackagedescribes = new List<VacinePackageDescribe>();

                    var statement1 = $"match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC {{Id:{i}}})-[r:Gom]->(vc:Vaccine) return vc,r.soLuong as soluong, d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi";
                    var result1 = session.Run(statement1);
                    packagevaccine.Id = i;
                    var a = result1.First();
                    packagevaccine.DanhMuc = a["tendm"].As<string>();
                    packagevaccine.GoiVaccine = a["tengoi"].As<string>();
                    packagevaccine.LoaiGoi = a["loaigoi"].As<string>();
                    var node = a["vc"].As<INode>();
                    vacinepackagedescribes.Add(new VacinePackageDescribe(new Vaccine(node), a["soluong"].As<int>()));
                    //var result2 = session.Run(statement2);
                    string describe = "";
                    foreach (var r1 in result1)
                    {
                        node = r1["vc"].As<INode>();
                        Vaccine temp = new Vaccine(node);
                        describe += temp.ProtectAgainst;
                        describe += ",";
                        vacinepackagedescribes.Add(new VacinePackageDescribe(temp, r1["soluong"].As<int>()));
                    }
                    packagevaccine.VacinePackageDescribes = vacinepackagedescribes;
                    packagevaccine.TotalPrice = vacinepackagedescribes.Sum(x => x.Vaccine.RetailPrice * x.Quantity);
                    packagevaccine.TotalCount = vacinepackagedescribes.Sum(x => x.Quantity);
                    packagevaccine.Describe = describe;

                    packagevaccines.Add(packagevaccine);
                }    
            _driver.CloseAsync();
            return packagevaccines;
        }
        }

        [HttpGet("{id}")]
        public VacinePackage GetPackage(int id)
        {
            VacinePackage packagevaccine = new VacinePackage();
            List<VacinePackageDescribe> vacinepackagedescribes = new List<VacinePackageDescribe>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = $"match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC {{Id:{id}}})-[r:Gom]->(vc:Vaccine) return vc,r.soLuong as soluong, d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi";
                var result = session.Run(statement);
                packagevaccine.Id = id;
                var a = result.First();
                packagevaccine.DanhMuc = a["tendm"].As<string>();
                packagevaccine.GoiVaccine = a["tengoi"].As<string>();
                packagevaccine.LoaiGoi = a["loaigoi"].As<string>();
                var node = a["vc"].As<INode>();
                vacinepackagedescribes.Add(new VacinePackageDescribe(new Vaccine(node), a["soluong"].As<int>()));
                //var result2 = session.Run(statement2);
                string describe="";
                foreach (var r in result)
                {                   
                    node = r["vc"].As<INode>();
                    Vaccine i = new Vaccine(node);
                    describe += i.ProtectAgainst;
                    describe += ",";
                    vacinepackagedescribes.Add(new VacinePackageDescribe(i, r["soluong"].As<int>()));
                }              
                packagevaccine.VacinePackageDescribes = vacinepackagedescribes;
                packagevaccine.TotalPrice = vacinepackagedescribes.Sum(x => x.Vaccine.RetailPrice * x.Quantity);
                packagevaccine.TotalCount = vacinepackagedescribes.Sum(x => x.Quantity);
                packagevaccine.Describe = describe;
            }
            _driver.CloseAsync();
            return packagevaccine;
        }

        [HttpGet("danhmuc")]
        public List<string> GetAllNamePackage()
        {
            List<string> danhmuc = new List<string>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = $"match (d:DanhMuc) return d.tenDM as tendm";
                var result = session.Run(statement);
                foreach (var r in result)
                {
                    string temp = r["tendm"].As<string>();
                    danhmuc.Add(temp);
                }
            }
            _driver.CloseAsync();
            return danhmuc;
        }
    }
}
