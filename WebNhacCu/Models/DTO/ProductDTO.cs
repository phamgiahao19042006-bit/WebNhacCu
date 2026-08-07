namespace WebNhacCu.Models.DTO
{
    public class ProductDTO
    {
        public string MaSP { get; set; } = string.Empty;

        public string TenSP { get; set; } = string.Empty;

        public decimal DonGia { get; set; }

        public int SoLuongTon { get; set; }

        public string? HinhAnh { get; set; }

        public string? MoTa { get; set; }

        public bool TT { get; set; }

        // Loại sản phẩm
        public string MaLoai { get; set; } = string.Empty;

        public string TenLoai { get; set; } = string.Empty;

        // Thương hiệu
        public string MaTH { get; set; } = string.Empty;

        public string TenTH { get; set; } = string.Empty;
    }
}