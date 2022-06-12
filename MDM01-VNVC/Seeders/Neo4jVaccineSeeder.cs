using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using MDM01_VNVC.Models;
using Neo4j.Driver;

namespace MDM01_VNVC.Seeders
{
    public class Neo4jVaccineSeeder
    {
        private static List<string[]> vaccines = new List<string[]>(){
                new string[] { "Bạch hầu, ho gà, uốn ván, bại liệt và Hib","Infanrix IPV+Hib","Bỉ","785.000","942.000","Có" },
                new string[] { "Bạch hầu, ho gà, uốn ván, bại liệt, Hib và viêm gan B","Infanrix Hexa (6in1)","Bỉ","1.015.000","1.218.000","Có" },
                new string[] { "Bạch hầu, ho gà, uốn ván, bại liệt, Hib và viêm gan B","Infanrix Hexa (6in1)","Pháp","1.048.000","1.258.000","Có" },
                new string[] { "Rota virus","Rotateq","Mỹ","665.000","798.000","Có" },
                new string[] { "Rota virus","Rotateq","Bỉ","825.000","990.000","Có" },
                new string[] { "Rota virus","Rotateq","Việt Nam","490.000","588.000","Có" },
                new string[] { "Các bệnh do phế cầu","Synflorix","Bỉ","1.045.000","1.254.000","Có" },
                new string[] { "Các bệnh do phế cầu","Synflorix","Bỉ","1.290.000","1.548.000","Có" },
                new string[] { "Lao","BCG","Việt Nam","125.000","150.000","Có" },
                new string[] { "Viêm gan B người lớn","Engerix B 1ml","Bỉ","235.000","282.000","Có" },
                new string[] { "Viêm gan B người lớn","Engerix B 1ml","Hàn Quốc","170.000","204.000","Có" },
                new string[] { "Viêm gan B trẻ em","Euvax B 0.5ml","Hàn Quốc","116.000","140.000","Có" },
                new string[] { "Viêm gan B trẻ em","Euvax B 0.5ml","Bỉ","190.000","228.000","Có" },
                new string[] { "Viêm màng não mô cầu BC","VA-Mengoc-BC","Cu Ba","295.000","354.000","Có" },
                new string[] { "Viêm màng não mô cầu ACYW","Menactra","Mỹ","1.260.000","1.512.000","Có" },
                new string[] { "Sởi","MVVac (Lọ 5ml)","Việt Nam","315.000","378.000","Có" },
                new string[] { "Sởi","MVVac (Lọ 5ml)","Việt Nam","180.000","216.000","Có" },
                new string[] { "Sởi – Quai bị – Rubella","MMR II (3 in 1)","Mỹ","305.000","366.000","Có" },
                new string[] { "Thủy đậu","Varivax","Mỹ","915.000","1.098.000","Có" },
                new string[] { "Thủy đậu","Varivax","Bỉ","945.000","1.134.000","Có" },
                new string[] { "Thủy đậu","Varivax","Hàn Quốc","700.000","840.000","Có" },
                new string[] { "Cúm","Vaxigrip Tetra 0.5ml","Pháp","356.000","428.000","Có (*)" },
                new string[] { "Cúm","Vaxigrip Tetra 0.5ml","Hà Lan","356.000","428.000","Có" },
                new string[] { "Cúm","Vaxigrip Tetra 0.5ml","Hàn Quốc","345.000","414.000","Có" },
                new string[] { "Cúm (người lớn > 18 tuổi)","Ivacflu-S 0,5ml","Việt Nam","190.000","228.000","Có" },
                new string[] { "Ung thư cổ tử cung và sùi mào gà","Gardasil","Mỹ","1.790.000","2.148.000","Có" },
                new string[] { "Ung thư cổ tử cung và sùi mào gà","Gardasil","Mỹ","2.950.000","3.540.000","Có" },
                new string[] { "Phòng uốn ván","VAT","Việt Nam","115.000","138.000","Có" },
                new string[] { "Phòng uốn ván","VAT","Việt Nam","100.000","120.000","Có" },
                new string[] { "Viêm não Nhật Bản","Imojev","Thái Lan","665.000","798.000","Có" },
                new string[] { "Viêm não Nhật Bản","Imojev","Việt Nam","170.000","204.000","Có" },
                new string[] { "Vắc xin phòng dại","Verorab 0,5ml (TB, TTD)","Pháp","323.000","388.000","Có" },
                new string[] { "Vắc xin phòng dại","Verorab 0,5ml (TB, TTD)","Ấn Độ","255.000","306.000","Có" },
                new string[] { "Vắc xin phòng dại","Verorab 0,5ml (TB, TTD)","Ấn Độ","215.000","258.000","Có" },
                new string[] { "Bạch hầu – Uốn ván – Ho gà","Adacel","Canada","620.000","744.000","Có" },
                new string[] { "Bạch hầu – Uốn ván – Ho gà","Adacel","Bỉ","735.000","882.000","Có" },
                new string[] { "Bạch hầu – Ho gà – Uốn ván – Bại liệt","Tetraxim","Pháp","458.000","550.000","Có" },
                new string[] { "Bạch hầu – Uốn ván","Uốn ván, bạch hầu hấp phụ (Td)-Lọ 0,5ml","Việt Nam","125.000","150.000","Có" },
                new string[] { "Bạch hầu – Uốn ván","Uốn ván, bạch hầu hấp phụ (Td)-Lọ 0,5ml","Việt Nam","95.000","114.000","Có" },
                new string[] { "Bạch hầu – Uốn ván","Uốn ván, bạch hầu hấp phụ (Td)-Lọ 0,5ml","Việt Nam","580.000","696.000","Có" },
                new string[] { "Viêm gan B và Viêm gan A","Twinrix","Bỉ","560.000","672.000","Có" },
                new string[] { "Viêm gan A","Havax 0,5ml","Việt Nam","235.000","282.000","Có" },
                new string[] { "Viêm gan A","Havax 0,5ml","Pháp","590.000","708.000","Có" },
                new string[] { "Thương hàn","Typhoid VI","Việt Nam","145.000","174.000","Có" },
                new string[] { "Thương hàn","Typhoid VI","Pháp","300.000","360.000","Có" },
                new string[] { "Các bệnh do Hib","Quimi-Hib","Cu Ba","239.000","287.000","Có" },
                new string[] { "Tả","mORCVAX","Việt Nam","115.000","138.000","Có" }
            };

        public static void Seed()
        {
            IDriver _driver = GraphDatabase.Driver("bolt://localhost:7687",
                AuthTokens.Basic("neo4j", "neo4j"));
            using (var session = _driver.Session())
            {
                var countResults = session.Run(
                    "MATCH (v:Vaccine) " +
                    "RETURN count(v) as cnt");
                foreach (var countResult in countResults) {
                    var count = countResult.Values["cnt"];
                    Console.WriteLine($"There are currently {count} Vaccines in Neo4j");
                    if ((long)count > 0)
                    {
                        _driver.CloseAsync();
                        return;
                    }
                    break;
                }

                Console.WriteLine("Begin populating Vaccines data into Neo4j");
                var writeTrans = session.WriteTransaction(tx =>
                {
                    List<IResult> results = new List<IResult>();
                    foreach (string[] vacccineInfo in vaccines)
                    {
                        Vaccine vac = new Vaccine(vacccineInfo[0],
                            vacccineInfo[1],
                            vacccineInfo[2],
                            double.Parse(vacccineInfo[3].Replace(".", "")),
                            double.Parse(vacccineInfo[4].Replace(".", "")),
                            vacccineInfo[5]);
                        var serializedVac = JsonSerializer.Serialize(vac).ToString();
                        string regexPattern = "\"([^\"]+)\":";
                        var attributes = Regex.Replace(serializedVac, regexPattern, "$1:");
                        var stmt = $"CREATE (v:Vaccine {attributes});";
                        Console.WriteLine(stmt);
                        var res = tx.Run(stmt);
                        results.Add(res);
                    }
                    return results;
                });
                Console.WriteLine("End populating Vaccines data into Neo4j");
                Console.WriteLine(vaccines.Count + " Vaccines populated.");
            }
            _driver.CloseAsync();
        }
    }
}
