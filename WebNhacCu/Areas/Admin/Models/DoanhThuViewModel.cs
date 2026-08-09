using System;
using System.Collections.Generic;

namespace CoreDatabase.ViewModels
{
    // Chứa thông tin 1 dòng trong bảng chi tiết
    public class DoanhThuChiTietModel
    {
        public string ThoiGian { get; set; } // Ví dụ: "01/06/2026" hoặc "Tháng 06/2026"
        public decimal DoanhThu { get; set; }
        public int SoDonHang { get; set; }
        public decimal GiamGia { get; set; }
    }

    // Model tổng truyền ra View
    public class DoanhThuReportViewModel
    {
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string NhomTheo { get; set; } // "Ngay", "Thang", "Nam"

        // 4 Thẻ Summary Cards
        public decimal TongDoanhThu { get; set; }
        public int TongDonHang { get; set; }
        public decimal GiaTriDonTrungBinh => TongDonHang > 0 ? TongDoanhThu / TongDonHang : 0;
        public decimal TongGiamGia { get; set; }

        // Danh sách dữ liệu cho Bảng & Biểu đồ
        public List<DoanhThuChiTietModel> ChiTietDoanhThu { get; set; } = new List<DoanhThuChiTietModel>();
    }
}