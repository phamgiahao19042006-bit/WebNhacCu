using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
namespace WebNhacCu.Models.EF
{
    public class WebHeThongBanNhacCuContext : DbContext
    {
        public WebHeThongBanNhacCuContext(DbContextOptions<WebHeThongBanNhacCuContext> options) : base(options)
        {
        }
        // --- 1. Phân hệ Sản phẩm ---
        public DbSet<LoaiSP> LoaiSPs { get; set; } = null!;
        public DbSet<ThuongHieu> ThuongHieus { get; set; } = null!;
        public DbSet<SanPham> SanPhams { get; set; } = null!;
        
        // --- 2. Phân hệ Nhân sự & Tài khoản ---
        public DbSet<NhanVien> NhanViens { get; set; } = null!;
        public DbSet<VaiTro> VaiTros { get; set; } = null!;
        public DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        
        // --- 3. Phân hệ Khách hàng ---
        public DbSet<KhachHang> KhachHangs { get; set; } = null!;
        
        // --- 4. Phân hệ Bán hàng ---
        public DbSet<HoaDon> HoaDons { get; set; } = null!;
        public DbSet<CTHoaDon> CTHoaDons { get; set; } = null!;
        
        // --- 5. Phân hệ Kho ---
        public DbSet<NhaCC> NhaCCs { get; set; } = null!;
        public DbSet<PhieuNhap> PhieuNhaps { get; set; } = null!;
        public DbSet<CTPhieuNhap> CTPhieuNhaps { get; set; } = null!;
        
        // --- 6. Phân hệ Khuyến mãi ---
        public DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        public DbSet<CTKhuyenMai> CTKhuyenMais { get; set; } = null!;
    }
}
