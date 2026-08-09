using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Areas.Admin.Models;
using WebNhacCu.Models.EF; // Namespace DbContext của bạn

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Đổi tên DbContext của bạn

        public HomeController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;

            var dashboardData = new DashboardViewModel
            {
                // 1. Tính tổng tiền các hóa đơn thành công trong tháng này
                DoanhThuThang = await _context.HoaDons
                    .Where(h => h.NgayLap.Month == now.Month && h.NgayLap.Year == now.Year)
                    .SumAsync(h => (decimal?)h.TongTien) ?? 0,

                // 2. Đếm số hóa đơn mới lập trong tháng này
                HoaDonMoi = await _context.HoaDons
                    .CountAsync(h => h.NgayLap.Month == now.Month && h.NgayLap.Year == now.Year),

                // 3. Tổng số lượng sản phẩm đang tồn kho
                SanPhamTonKho = await _context.SanPhams
                    .SumAsync(s => (int?)s.SoLuongTon) ?? 0,

                // 4. Đếm số khách hàng mới đăng ký trong tháng này
                KhachHangMoi = await _context.KhachHangs.CountAsync(k => k.CreatedDate.Month == now.Month && k.CreatedDate.Year == now.Year)
                
            };
            return View(dashboardData);
        }
        

        // --- API TÌM KIẾM NHANH TOPBAR (FIX LỖI LINQ & SQL) ---
        [HttpGet]
        public async Task<IActionResult> QuickSearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Json(new { success = false, message = "Từ khóa rỗng" });
            }

            keyword = keyword.Trim().ToLower();

            // 1. Tìm Sản phẩm
            var rawSanPhams = await _context.SanPhams
                .Where(p => p.TenSP.ToLower().Contains(keyword) || p.MaSP.ToLower().Contains(keyword))
                .Take(4)
                .ToListAsync(); // Lấy dữ liệu từ SQL về trước

            var sanPhams = rawSanPhams.Select(p => new {
                id = p.MaSP,
                title = p.TenSP,
                subtitle = $"Giá: {p.DonGia:N0} ₫",
                type = "Sản phẩm",
                icon = "fa-guitar",
                url = $"/Admin/SanPham/Details/{p.MaSP}"
            }).ToList();

            // 2. Tìm Hóa đơn (MaHD kiểu string)
            var rawHoaDons = await _context.HoaDons
                .Where(h => h.MaHD.ToLower().Contains(keyword))
                .Take(4)
                .ToListAsync();

            var hoaDons = rawHoaDons.Select(h => new {
                id = h.MaHD,
                title = $"Hóa đơn #{h.MaHD}",
                subtitle = $"Tổng tiền: {h.TongTien:N0} ₫ - Ngày: {h.NgayLap:dd/MM/yyyy}",
                type = "Đơn hàng",
                icon = "fa-receipt",
                url = $"/Admin/BanHang/ChiTietHoaDon/{h.MaHD}"
            }).ToList();

            // 3. Tìm Khách hàng
            var rawKhachHangs = await _context.KhachHangs
                .Where(k => k.HoTen.ToLower().Contains(keyword) || k.SDT.Contains(keyword) || k.MaKH.ToLower().Contains(keyword))
                .Take(4)
                .ToListAsync();

            var khachHangs = rawKhachHangs.Select(k => new {
                id = k.MaKH,
                title = k.HoTen,
                subtitle = $"SĐT: {k.SDT}",
                type = "Khách hàng",
                icon = "fa-user",
                url = $"/Admin/KhachHang/Details/{k.MaKH}"
            }).ToList();

            // Tổng hợp kết quả
            var results = sanPhams.Cast<object>()
                .Concat(hoaDons)
                .Concat(khachHangs);

            return Json(new { success = true, data = results });
        }
        // Thêm [Route("Logout")] 
        [Route("Logout")]
        public IActionResult Logout()
        {
            // Kiểm tra an toàn xem Session có khả dụng không mới Clear
            if (HttpContext.Session != null)
            {
                HttpContext.Session.Clear();
            }

            // Chuyển hướng thẳng về trang chủ (Hình 1)
            return Redirect("/");
        }
    }
}