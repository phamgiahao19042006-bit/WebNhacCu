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
    public class SanPhamBanChayController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // ⚠️ Đổi tên YourDbContext thành DbContext của Hào

        public SanPhamBanChayController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // Action nhận đường dẫn: /Admin/SanPhamBanChay
        [HttpGet]
        public async Task<IActionResult> Index(DateTime? tuNgay, DateTime? denNgay, int top = 10, string nhomTheo = "SoLuong")
        {
            var startDate = tuNgay ?? DateTime.Now.AddDays(-30).Date;
            var endDate = denNgay ?? DateTime.Now.Date;

            // 📌 TRUY VẤN TỪ BẢNG CHI TIẾT HÓA ĐƠN
            var query = _context.CTHoaDons
                .Include(ct => ct.HoaDon)
                .Include(ct => ct.SanPham)
                .ThenInclude(sp => sp.LoaiSP)
                .Where(ct => ct.HoaDon.NgayLap >= startDate && ct.HoaDon.NgayLap <= endDate.AddDays(1).AddTicks(-1));

            var groupedQuery = query.GroupBy(ct => new
            {
                ct.MaSP,
                ct.SanPham.TenSP,
                LoaiSP = ct.SanPham.LoaiSP != null ? ct.SanPham.LoaiSP.TenLoai : "Khác",
                ThuongHieu = ct.SanPham.ThuongHieu != null ? ct.SanPham.ThuongHieu.TenTH : "OEM"
            })
            .Select(g => new SanPhamBanChayItemModel
            {
                MaSP = g.Key.MaSP,
                TenSP = g.Key.TenSP,
                LoaiSP = g.Key.LoaiSP,
                ThuongHieu = g.Key.ThuongHieu,
                SoLuongBan = g.Sum(x => x.SoLuong),
                DoanhThu = g.Sum(x => x.SoLuong * x.DonGia)
            });

            if (nhomTheo == "DoanhThu")
            {
                groupedQuery = groupedQuery.OrderByDescending(x => x.DoanhThu);
            }
            else
            {
                groupedQuery = groupedQuery.OrderByDescending(x => x.SoLuongBan);
            }

            var listResult = await groupedQuery.Take(top).ToListAsync();

            int stt = 1;
            foreach (var item in listResult)
            {
                item.STT = stt++;
            }

            var viewModel = new SanPhamBanChayViewModel
            {
                TuNgay = startDate,
                DenNgay = endDate,
                Top = top,
                NhomTheo = nhomTheo,
                DanhSachSanPham = listResult
            };

            return View(viewModel);
        }
    }
}