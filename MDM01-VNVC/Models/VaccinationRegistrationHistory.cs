using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDM01_VNVC.Models
{
    [BsonIgnoreExtraElements]
    public class VaccinationRegistrationHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("NguoiTiem_HoTen")]
        public string NguoiTiem_HoTen { get; set; }

        [BsonElement("NguoiTiem_NgaySinh")]
        public string NguoiTiem_NgaySinh { get; set; }

        [BsonElement("NguoiTiem_SDT")]
        public string NguoiTiem_SDT { get; set; }

        [BsonElement("NguoiTiem_GioiTinh")]
        public string NguoiTiem_GioiTinh { get; set; }

        [BsonElement("NguoiTiem_DiaChi_SoNha_Duong")]
        public string NguoiTiem_DiaChi_SoNha_Duong { get; set; }

        [BsonElement("NguoiTiem_DiaChi_Phuong")]
        public string NguoiTiem_DiaChi_Phuong { get; set; }

        [BsonElement("NguoiTiem_DiaChi_Quan")]
        public string NguoiTiem_DiaChi_Quan { get; set; }

        [BsonElement("NguoiTiem_DiaChi_Tinh")]
        public string NguoiTiem_DiaChi_Tinh { get; set; }

        [BsonElement("NguoiLienHe_HoTen")]
        public string NguoiLienHe_HoTen { get; set; }

        [BsonElement("NguoiLienHe_MoiQuanHe")]
        public string NguoiLienHe_MoiQuanHe { get; set; }

        [BsonElement("NguoiLienHe_SDT")]
        public string NguoiLienHe_SDT { get; set; }

        [BsonElement("ThongTinTiem_LoaiVC")]
        public string ThongTinTiem_LoaiVC { get; set; }

        [BsonElement("ThongTinTiem_TenVC")]
        public string ThongTinTiem_TenVC { get; set; }

        [BsonElement("ThongTinTiem_TrungTamTiem")]
        public string ThongTinTiem_TrungTamTiem { get; set; }

        [BsonElement("ThongTinTiem_NgayDuDinhTiem")]
        public string ThongTinTiem_NgayDuDinhTiem { get; set; } 


        public VaccinationRegistrationHistory(string NguoiTiem_HoTen, string NguoiTiem_NgaySinh, string NguoiTiem_SDT, string NguoiTiem_GioiTinh, string NguoiTiem_DiaChi_SoNhaDuong, string NguoiTiem_DiaChi_Phuong, string NguoiTiem_DiaChi_Quan, string NguoiTiem_DiaChi_Tinh, string NguoiLienHe_HoTen, string NguoiLienHe_MoiQuanHe, string NguoiLienHe_SDT, string ThongTinTiem_LoaiVC, string ThongTinTiem_TenVC, string ThongTinTiem_TrungTamTiem, string ThongTinTiem_NgayDuDinhTiem)
        {
            this.NguoiTiem_HoTen = NguoiTiem_HoTen;
            this.NguoiTiem_NgaySinh = NguoiTiem_NgaySinh;
            this.NguoiTiem_SDT = NguoiTiem_SDT;
            this.NguoiTiem_GioiTinh = NguoiTiem_GioiTinh;
            this.NguoiTiem_DiaChi_SoNha_Duong = NguoiTiem_DiaChi_SoNha_Duong;
            this.NguoiTiem_DiaChi_Phuong = NguoiTiem_DiaChi_Phuong;
            this.NguoiTiem_DiaChi_Quan = NguoiTiem_DiaChi_Quan;
            this.NguoiTiem_DiaChi_Tinh = NguoiTiem_DiaChi_Tinh;
            this.NguoiLienHe_HoTen = NguoiLienHe_HoTen;
            this.NguoiLienHe_MoiQuanHe = NguoiLienHe_MoiQuanHe;
            this.NguoiLienHe_SDT = NguoiLienHe_SDT;
            this.ThongTinTiem_LoaiVC = ThongTinTiem_LoaiVC;
            this.ThongTinTiem_TenVC = ThongTinTiem_TenVC;
            this.ThongTinTiem_TrungTamTiem = ThongTinTiem_TrungTamTiem;
            this.ThongTinTiem_NgayDuDinhTiem = ThongTinTiem_NgayDuDinhTiem;
        }
    }
}
