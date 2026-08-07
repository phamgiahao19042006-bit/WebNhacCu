using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietPhieuNhapController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public ChiTietPhieuNhapController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. Danh sách chi tiết nhập hàng (Lọc theo Mã Phiếu Nhập nếu có)
        public async Task<IActionResult> Index(string maPN)
        {
            ViewBag.MaPN = maPN;
            ViewBag.DsPhieuNhap = await _context.PhieuNhaps.ToListAsync();

            var query = _context.CTPhieuNhaps.AsQueryable();

            if (!string.IsNullOrEmpty(maPN))
            {
                query = query.Where(c => c.MaPN == maPN);
            }

            return View(await query.ToListAsync());
        }

        // 2. Thêm mới chi tiết nhập hàng (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CTPhieuNhap ct)
        {
            try
            {
                _context.CTPhieuNhaps.Add(ct);
                await _context.SaveChangesAsync();

                // Cập nhật lại Tổng tiền cho Phiếu nhập
                await CapNhatTongTienPhieuNhap(ct.MaPN);

                TempData["SuccessMessage"] = "Thêm chi tiết nhập hàng thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi thêm: " + ex.Message;
            }

            return RedirectToAction("Index", new { maPN = ct.MaPN });
        }

        // 3. Xóa chi tiết nhập hàng (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(string maPN, string maSP)
        {
            var ct = await _context.CTPhieuNhaps
                .FirstOrDefaultAsync(c => c.MaPN == maPN && c.MaSP == maSP);

            if (ct != null)
            {
                _context.CTPhieuNhaps.Remove(ct);
                await _context.SaveChangesAsync();

                // Cập nhật lại Tổng tiền cho Phiếu nhập
                await CapNhatTongTienPhieuNhap(maPN);

                return Json(new { success = true, message = "Đã xóa chi tiết nhập hàng thành công!" });
            }

            return Json(new { success = false, message = "Không tìm thấy dữ liệu!" });
        }

        // Hàm hỗ trợ tự động tính toán lại Tổng tiền của Phiếu nhập
        private async Task CapNhatTongTienPhieuNhap(string maPN)
        {
            var phieuNhap = await _context.PhieuNhaps.FindAsync(maPN);
            if (phieuNhap != null)
            {
                var tongTien = await _context.CTPhieuNhaps
                    .Where(c => c.MaPN == maPN)
                    .SumAsync(c => c.SoLuong * c.DonGiaNhap);

                phieuNhap.TongTien = tongTien;
                await _context.SaveChangesAsync();
            }
        }
    }
}