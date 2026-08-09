using System;
using System.Collections.Generic;

namespace WebNhacCu.Models.ViewModels
{
    public class CTHoaDonItem
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }

    public class LapHoaDonViewModel
    {
        public string MaHD { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public string PhuongThucTT { get; set; }
        public decimal GiamGia { get; set; } = 0;
        public int TrangThai { get; set; } = 1; // 1: Đã thanh toán, 0: Chưa thanh toán

        // Danh sách các chi tiết sản phẩm chọn mua
        public List<CTHoaDonItem> ChiTiet { get; set; } = new List<CTHoaDonItem>();
    }
}