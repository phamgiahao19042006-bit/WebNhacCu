namespace WebNhacCu.Models
{
    public class CTHoaDonItem
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        // Thêm { get; set; } đầy đủ như thế này:
        public decimal ThanhTien { get; set; }
    }

    public class CreateHoaDonViewModel
    {
        public string MaHD { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public decimal TongTien { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string PhuongThucTT { get; set; } // Tiền mặt, Chuyển khoản...
        public bool TT { get; set; } = true; // Trạng thái: Đã thanh toán / Chưa

        public List<CTHoaDonItem> ChiTiet { get; set; } = new List<CTHoaDonItem>();
    }
}