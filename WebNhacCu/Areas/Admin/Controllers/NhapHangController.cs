using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebNhacCu.Models.EF;
using WebNhacCu.Models.ViewModels;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhapHangController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Đổi tên DbContext đúng với dự án của Hào

        public NhapHangController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // Đường dẫn: /Admin/NhapHang
        [HttpGet]
        public async Task<IActionResult> Index(DateTime? tuNgay, DateTime? denNgay, string maNCC, string trangThai)
        {
            var model = new NhapHangViewModel
            {
                TừNgay = tuNgay ?? new DateTime(2025, 6, 1),
                ĐếnNgay = denNgay ?? DateTime.Now,
                MaNCC = maNCC,
                TrangThaiLoc = trangThai
            };

            // 1. Truy vấn danh sách phiếu nhập
            var query = _context.PhieuNhaps
                .Include(p => p.NhaCC)
                .Include(p => p.CTPhieuNhaps)
                .Where(p => p.NgayNhap >= model.TừNgay && p.NgayNhap <= model.ĐếnNgay);

            if (!string.IsNullOrEmpty(maNCC) && maNCC != "Tất cả")
            {
                query = query.Where(p => p.MaNCC == maNCC);
            }

            var rawData = await query.ToListAsync();

            // 2. Tính toán các thẻ thống kê KPI
            model.TongPhieuNhap = rawData.Count;
            model.TongGiaTriNhap = rawData.Sum(p => p.TongTien);
            model.TongSanPhamNhap = rawData.SelectMany(p => p.CTPhieuNhaps).Sum(ct => ct.SoLuong);
            model.TongNhaCungCap = rawData.Select(p => p.MaNCC).Distinct().Count();

            // 3. Mapping dữ liệu ra View
            model.DanhSachPhieuNhap = rawData.Select(p => {
                decimal tong = p.TongTien;
                decimal daTT = tong;
                decimal conNo = tong - daTT;

                string tt = "Hoàn thành";
                if (daTT == 0 && tong > 0) tt = "Chưa thanh toán";
                else if (conNo > 0) tt = "Còn nợ";

                return new PhieuNhapItem
                {
                    MaPN = p.MaPN,
                    NgayNhap = p.NgayNhap,
                    TenNhaCungCap = p.NhaCC.TenNCC ?? "N/A",
                    MaNV = "NV001",
                    TongTien = tong,
                    DaThanhToan = daTT,
                    TrangThai = tt
                };
            }).ToList();

            // 4. Lọc theo trạng thái
            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                model.DanhSachPhieuNhap = model.DanhSachPhieuNhap
                    .Where(p => p.TrangThai.Equals(trangThai, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.NhaCungCapList = await _context.NhaCCs.ToListAsync();
            return View(model);
        }
    }
}