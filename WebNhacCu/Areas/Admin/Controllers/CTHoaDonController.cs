using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CTHoaDonController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public CTHoaDonController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // ==========================================
        // 2. CHỨC NĂNG: CHI TIẾT HÓA ĐƠN
        // URL: /Admin/CTHoaDon hoặc /Admin/CTHoaDon/Index
        // ==========================================
        public IActionResult Index(string maHD)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                return RedirectToAction("Index", "HoaDon", new { area = "Admin" });
            }

            // 1. Lấy hóa đơn
            var hoaDon = _context.HoaDons.FirstOrDefault(h => h.MaHD == maHD);
            if (hoaDon == null) return NotFound();

            // 2. Query danh sách chi tiết sản phẩm từ Database
            var chiTietList = (from ct in _context.CTHoaDons
                               join sp in _context.SanPhams on ct.MaSP equals sp.MaSP
                               where ct.MaHD == maHD
                               select new
                               {
                                   ct.MaSP,
                                   sp.TenSP,
                                   ct.DonGia,
                                   ct.SoLuong,
                                   ct.ThanhTien
                               }).ToList();

            ViewBag.ChiTietHoaDon = chiTietList;

            return View(hoaDon);
        }

        // Lưu sản phẩm vào Chi tiết hóa đơn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CTHoaDon ct)
        {
            try
            {
                // Tự động tính Thành Tiền = Số Lượng * Đơn Giá
                ct.ThanhTien = ct.SoLuong * ct.DonGia;

                var existing = await _context.CTHoaDons
                    .FirstOrDefaultAsync(c => c.MaHD == ct.MaHD && c.MaSP == ct.MaSP);

                if (existing != null)
                {
                    existing.SoLuong += ct.SoLuong;
                    existing.DonGia = ct.DonGia;
                    existing.ThanhTien = existing.SoLuong * existing.DonGia;
                    _context.Entry(existing).State = EntityState.Modified;
                }
                else
                {
                    _context.CTHoaDons.Add(ct);
                }

                // Cập nhật lại Tổng tiền bên bảng HoaDon
                await _context.SaveChangesAsync();
                await UpdateTongTienHoaDon(ct.MaHD);

                TempData["SuccessMessage"] = "Thêm sản phẩm vào chi tiết hóa đơn thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi lưu chi tiết: " + ex.Message;
            }

            return RedirectToAction(nameof(Index), new { maHD = ct.MaHD });
        }

        private async Task UpdateTongTienHoaDon(string maHD)
        {
            var hoaDon = await _context.HoaDons.FindAsync(maHD);
            if (hoaDon != null)
            {
                var tongTien = await _context.CTHoaDons
                    .Where(c => c.MaHD == maHD)
                    .SumAsync(c => c.ThanhTien);

                hoaDon.TongTien = tongTien;
                hoaDon.ThanhTien = tongTien - hoaDon.GiamGia;
                await _context.SaveChangesAsync();
            }
        }
        
    }
}