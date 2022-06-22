:use vnvc;
create (dm:DanhMuc {Id: 1,tenDM:"Vắc xin cho trẻ em / 0-9 Tháng"});
create (gv:GoiVC {Id: 1,tenGoiVC:"GÓI VẮC XIN Infanrix (0-9 tháng)"});
create (l:LoaiGoiVC {Id: 1,tenLoai:"GÓI LINH ĐỘNG 1"});
create (l:LoaiGoiVC {Id:2,tenLoai:"GÓI LINH ĐỘNG 2"});

match (l:LoaiGoiVC {Id: 1}),(gv:GoiVC {Id: 1}) create (gv)-[:Co]->(l) return gv,l;
match (l:LoaiGoiVC {Id: 2}),(gv:GoiVC {Id: 1}) create (gv)-[:Co]->(l) return gv,l
match (dm:DanhMuc {Id: 1}),(gv:GoiVC {Id: 1}) create (gv)-[:Thuoc]->(dm) return gv,dm

match (v:Vaccine {Name:"Rotateq"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Hexaxim"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: 2}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

match (v:Vaccine {Name:"Rotarix"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Hexaxim"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: 1}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

create (gv:GoiVC {Id: 2,tenGoiVC:"Gói vắc xin Pentaxim (0-9 tháng)"});
create (l:LoaiGoiVC {Id: 3,tenLoai:"GÓI LINH ĐỘNG 1"});
create (l:LoaiGoiVC {Id:4,tenLoai:"GÓI LINH ĐỘNG 2"});

match (l:LoaiGoiVC {Id: 3}),(gv:GoiVC {Id: 2}) create (gv)-[:Co]->(l) return gv,l;
match (l:LoaiGoiVC {Id: 4}),(gv:GoiVC {Id: 2}) create (gv)-[:Co]->(l) return gv,l;
match (dm:DanhMuc {Id: 1}),(gv:GoiVC {Id: 2}) create (gv)-[:Thuoc]->(dm) return gv,dm;

match (v:Vaccine {Name:"Rotateq"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Pentaxim"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Engerix B 0,5ml"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

match (v:Vaccine {Name:"Rotarix"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 2}]->(v) return v,l,r;
match (v:Vaccine {Name:"Pentaxim"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Engerix B 0,5ml"}),(l:LoaiGoiVC {Id: 3}) create (l)-[r:Gom{soLuong: 3}]->(v) return v,l,r;
match (v:Vaccine {Name:"Synflorix"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 4}]->(v) return v,l,r;
match (v:Vaccine {Name:"Vaxigrip tetra"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Mvvac"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Imojev"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;
match (v:Vaccine {Name:"Menactra"}),(l:LoaiGoiVC {Id: 4}) create (l)-[r:Gom{soLuong: 1}]->(v) return v,l,r;

create (dm:TinhThanh {Id: 1,tenTinhThanh:"HCM"});
create (dm:TinhThanh {Id: 2,tenTinhThanh:"HN"});

create (dm:QuanHuyen {Id: 1,tenQuanHuyen:"Q1"});
create (dm:QuanHuyen {Id: 2,tenQuanHuyen:"Q2"});

create (dm:PhuongXa {Id: 1,tenPhuongXa:"P1"});
create (dm:PhuongXa {Id: 2,tenPhuongXa:"P2"});
create (dm:PhuongXa {Id: 3,tenPhuongXa:"P3"});
create (dm:PhuongXa {Id: 4,tenPhuongXa:"P4"});

match (qh:QuanHuyen {Id: 1}),(tt:TinhThanh {Id: 1}) create (qh)-[:Trong]->(tt) return qh,tt;
match (qh:QuanHuyen {Id: 2}),(tt:TinhThanh {Id: 1}) create (qh)-[:Trong]->(tt) return qh,tt;

match (qh:QuanHuyen {Id: 1}),(px:PhuongXa {Id: 1}) create (qh)-[:Chua]->(px) return qh,px;
match (qh:QuanHuyen {Id: 1}),(px:PhuongXa {Id: 2}) create (qh)-[:Chua]->(px) return qh,px;

match (qh:QuanHuyen {Id: 2}),(px:PhuongXa {Id: 3}) create (qh)-[:Chua]->(px) return qh,px;
match (qh:QuanHuyen {Id: 2}),(px:PhuongXa {Id: 4}) create (qh)-[:Chua]->(px) return qh,px;
