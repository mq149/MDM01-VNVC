ALTER USER neo4j SET PASSWORD 'neo4j'
match (a) -[r] -> () delete a, r
match (a) delete a

create (dm:DanhMuc {Id: "1",tenDM:"Vắc xin cho trẻ em / 0-9 Tháng"});
create (dm:DanhMuc {Id: "2",tenDM:"Vắc xin cho trẻ em / 0-12 Tháng"});
create (gv:GoiVC {Id: "1",tenGoiVC:"GÓI VẮC XIN Infanrix (0-9 tháng)"});
create (l:LoaiGoiVC {Id: "1",tenLoai:"GÓI LINH ĐỘNG 1"});
create (l:LoaiGoiVC {Id:"2",tenLoai:"GÓI LINH ĐỘNG 2"});

match (l:LoaiGoiVC {Id: "1"}),(gv:GoiVC {Id: "1"}) create (gv)-[:Co]->(l) return gv,l;
match (l:LoaiGoiVC {Id: "2"}),(gv:GoiVC {Id: "1"}) create (gv)-[:Co]->(l) return gv,l;
match (dm:DanhMuc {Id: "1"}),(gv:GoiVC {Id: "1"}) create (gv)-[:Thuoc]->(dm) return gv,dm;

match (v:Vaccine {Name:"Rotateq"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Hexaxim"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: "2"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

match (v:Vaccine {Name:"Rotarix"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Hexaxim"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: "1"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
///
create (gv:GoiVC {Id: "2",tenGoiVC:"Gói vắc xin Pentaxim (0-9 tháng)"});
create (l:LoaiGoiVC {Id: "3",tenLoai:"GÓI LINH ĐỘNG 1"});
create (l:LoaiGoiVC {Id:"4",tenLoai:"GÓI LINH ĐỘNG 2"});

match (l:LoaiGoiVC {Id: "3"}),(gv:GoiVC {Id: "2"}) create (gv)-[:Co]->(l) return gv,l;
match (l:LoaiGoiVC {Id: "4"}),(gv:GoiVC {Id: "2"}) create (gv)-[:Co]->(l) return gv,l;

match (dm:DanhMuc {Id: "1"}), (gv:GoiVC {Id: "2"}) create (gv)-[:Thuoc]->(dm) return gv,dm;
match (dm:DanhMuc {Id: "2"}) ,(gv:GoiVC {Id: "1"}) create (gv)-[:Thuoc]->(dm) return gv,dm;
match (dm:DanhMuc {Id: "2"}) ,(gv:GoiVC {Id: "2"}) create (gv)-[:Thuoc]->(dm) return gv,dm;

match (v:Vaccine {Name:"Rotateq"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Pentaxim"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Engerix B 0,5ml"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: "3"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

match (v:Vaccine {Name:"Rotarix"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Pentaxim"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Engerix B 0,5ml"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: "4"}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;


match (n:GoiVC{tenGoiVC:"GÓI VẮC XIN Infanrix (0-9 tháng)"})-[:Co]->(l:LoaiGoiVC{tenLoai:"GÓI LINH ĐỘNG 2"})-[r:Gom]->(vc:Vaccine) return n,l,vc,r


