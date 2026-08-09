using System;
using System.Collections.Generic;

namespace WebNhacCu.Models.ViewModels
{
    public class NhapHangViewModel
    {
        public DateTime TừNgay { get; set; }
        public DateTime ĐếnNgay { get; set; }
        public string MaNCC { get; set; }
        public string TrangThaiLoc { get; set; }

        public int TongPhieuNhap { get; set; }
        public decimal TongGiaTriNhap { get; set; }
        public int TongSanPhamNhap { get; set; }
        public int TongNhaCungCap { get; set; }

        public List<PhieuNhapItem> DanhSachPhieuNhap { get; set; } = new List<PhieuNhapItem>();
    }

    public class PhieuNhapItem
    {
        public string MaPN { get; set; }
        public DateTime NgayNhap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string MaNV { get; set; }
        public decimal TongTien { get; set; }
        public decimal DaThanhToan { get; set; }
        public decimal ConNo => TongTien - DaThanhToan;
        public string TrangThai { get; set; }
    }
}