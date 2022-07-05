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
                List<string> dsVc = new List<string>();
                foreach (var r in result)
                {
                    dsVc.Add(r["Id"].As<string>());
                }
                foreach (string i in dsVc)
                {
                    VacinePackage packagevaccine = new VacinePackage();
                    List<VacinePackageDescribe> vacinepackagedescribes = new List<VacinePackageDescribe>();
                    var statement1 = "match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC {Id:'"+i+"'})-[r:Gom]->(vc:Vaccine) return vc,r.soLuong as soluong, d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi";
                    var result1 = session.Run(statement1);
                    Console.WriteLine(statement1);
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
        public VacinePackage GetPackage(string id)
        {
            VacinePackage packagevaccine = new VacinePackage();
            List<VacinePackageDescribe> vacinepackagedescribes = new List<VacinePackageDescribe>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = "match (d:DanhMuc)<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC {Id:'"+id+"'})-[r:Gom]->(vc:Vaccine) return vc,r.soLuong as soluong, d.tenDM as tendm,g.tenGoiVC as tengoi ,l.tenLoai as loaigoi";
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
        public List<VacinePackage> GetAllNamePackage()
        {
            List<VacinePackage> danhmuc = new List<VacinePackage>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = $"match (d:DanhMuc) return d.Id as Id,d.tenDM as tendm";
                var result = session.Run(statement);
                foreach (var r in result)
                {
                    VacinePackage vacinepackage = new VacinePackage();
                    vacinepackage.Id = r["Id"].As<string>();
                    vacinepackage.DanhMuc = r["tendm"].As<string>();
                    danhmuc.Add(vacinepackage);
                }
            }
            _driver.CloseAsync();
            return danhmuc;
        }

        [HttpGet("danhmuc/{id}")]
        public List<VacinePackage> GetPackages(string id)
        {
            List<VacinePackage> packages = new List<VacinePackage>();
            using (var session = _driver.Session(sessionConfig))
            {
                var statement = "MATCH (d:DanhMuc {Id:'"+id+"'})<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC)-[r:Gom]->(vc:Vaccine)"+
                        "CALL { MATCH (d:DanhMuc {Id:'"+id+"'})<-[:Thuoc]-(g:GoiVC)-[:Co]->(l:LoaiGoiVC)-[r:Gom]->(vc:Vaccine)" +
                         "   RETURN collect(vc.ProtectAgainst) AS protectagainsts }"+
                        "RETURN DISTINCT l.Id AS Id,d.tenDM AS TenDanhMuc,g.tenGoiVC AS TenGoiVaccine,l.tenLoai AS TenLoaiGoi,"+
                        "count(vc) AS SoLuongVaccine,sum(r.soLuong) AS TongSoLieu,sum(r.soLuong*vc.RetailPrice) AS GiaGoi,protectagainsts as ProtectAgainsts";
                var result = session.Run(statement);
                Console.WriteLine(statement);
                foreach (var r in result)
                {
                    VacinePackage vacinepackage = new VacinePackage();
                    vacinepackage.Id = r["Id"].As<string>();
                    vacinepackage.DanhMuc = r["TenDanhMuc"].As<string>();
                    vacinepackage.GoiVaccine = r["TenGoiVaccine"].As<string>();
                    vacinepackage.LoaiGoi = r["TenLoaiGoi"].As<string>();
                    vacinepackage.DanhMuc = r["TenDanhMuc"].As<string>();
                    vacinepackage.TotalCount=r["TongSoLieu"].As<int>();
                    vacinepackage.TotalPrice = r["GiaGoi"].As<double>();
                    vacinepackage.Describe = String.Join(", ",r["ProtectAgainsts"].As<List<string>>());
                    packages.Add(vacinepackage);
                }
                //Console.WriteLine(packages[0].DanhMuc);
            }
            _driver.CloseAsync();
            return packages;
        }

    }
}
