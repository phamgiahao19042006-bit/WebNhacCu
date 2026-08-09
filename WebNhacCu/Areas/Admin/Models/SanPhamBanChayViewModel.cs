using System;
using System.Collections.Generic;

namespace CoreDatabase.ViewModels
{
    // Chứa thông tin từng sản phẩm bán chạy
    public class SanPhamBanChayItemModel
    {
        public int STT { get; set; }
        public string MaSP { get; set; } = string.Empty;
        public string TenSP { get; set; } = string.Empty;
        public string LoaiSP { get; set; } = string.Empty;
        public string ThuongHieu { get; set; } = string.Empty;
        public int SoLuongBan { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal GiaBanTB => SoLuongBan > 0 ? DoanhThu / SoLuongBan : 0;
    }

    // Model tổng truyền ra View
    public class SanPhamBanChayViewModel
    {
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int Top { get; set; } = 10;
        public string NhomTheo { get; set; } = "SoLuong"; // "SoLuong" hoặc "DoanhThu"

        // Danh sách hiển thị
        public List<SanPhamBanChayItemModel> DanhSachSanPham { get; set; } = new List<SanPhamBanChayItemModel>();

        // Dữ liệu hàng Tổng cộng
        public int TongSoLuongBan => DanhSachSanPham.Sum(x => x.SoLuongBan);
        public decimal TongDoanhThu => DanhSachSanPham.Sum(x => x.DoanhThu);
    }
}