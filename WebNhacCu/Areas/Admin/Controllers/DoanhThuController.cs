using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebNhacCu.Models.EF;

namespace CoreDatabase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoanhThuController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // ⚠️ Đổi tên YourDbContext thành DbContext của Hào

        public DoanhThuController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // Action mặc định nhận Route: /Admin/DoanhThu
        [HttpGet]
        public async Task<IActionResult> Index(DateTime? tuNgay, DateTime? denNgay, string nhomTheo = "Ngay")
        {
            var startDate = tuNgay ?? DateTime.Now.AddDays(-30).Date;
            var endDate = denNgay ?? DateTime.Now.Date;

            // 📌 LẤY DỮ LIỆU TỪ BẢNG HOA DON (Chỉ lấy hóa đơn đã thanh toán nếu có trạng thái)
            var hoaDons = await _context.HoaDons
                .Where(h => h.NgayLap >= startDate && h.NgayLap <= endDate.AddDays(1).AddTicks(-1)) // Đổi NgayTao thành NgayLap
                .ToListAsync();

            var viewModel = new DoanhThuReportViewModel
            {
                TuNgay = startDate,
                DenNgay = endDate,
                NhomTheo = nhomTheo,
                TongDoanhThu = hoaDons.Sum(h => h.TongTien), // Hoặc h.ThanhTien tùy tên cột trong DB
                TongDonHang = hoaDons.Count // Đếm số lượng Hóa đơn
            };

            // Nhóm dữ liệu theo Ngày / Tháng / Năm
            if (nhomTheo == "Thang")
            {
                viewModel.ChiTietDoanhThu = hoaDons
                    .GroupBy(h => h.NgayLap.ToString("MM/yyyy"))
                    .Select(g => new DoanhThuChiTietModel
                    {
                        ThoiGian = "Tháng " + g.Key,
                        DoanhThu = g.Sum(x => x.TongTien),
                        SoDonHang = g.Count()
                    }).ToList();
            }
            else if (nhomTheo == "Nam")
            {
                viewModel.ChiTietDoanhThu = hoaDons
                    .GroupBy(h => h.NgayLap.ToString("yyyy"))
                    .Select(g => new DoanhThuChiTietModel
                    {
                        ThoiGian = "Năm " + g.Key,
                        DoanhThu = g.Sum(x => x.TongTien),
                        SoDonHang = g.Count()
                    }).ToList();
            }
            else // Mặc định: Ngay
            {
                viewModel.ChiTietDoanhThu = hoaDons
                    .GroupBy(h => h.NgayLap.ToString("dd/MM/yyyy"))
                    .Select(g => new DoanhThuChiTietModel
                    {
                        ThoiGian = g.Key,
                        DoanhThu = g.Sum(x => x.TongTien),
                        SoDonHang = g.Count()
                    })
                    .OrderBy(x => DateTime.ParseExact(x.ThoiGian, "dd/MM/yyyy", null))
                    .ToList();
            }

            return View(viewModel);
        }
    }
}