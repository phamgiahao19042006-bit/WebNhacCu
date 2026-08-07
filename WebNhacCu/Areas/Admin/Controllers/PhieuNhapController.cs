using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhieuNhapController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public PhieuNhapController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. Danh sách Phiếu nhập
        public async Task<IActionResult> Index(string search, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.PhieuNhaps.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                // Chỉ lọc theo MaPN hoặc MaNCC
                query = query.Where(p => p.MaPN.Contains(search) || p.MaNCC.Contains(search));
            }

            if (fromDate.HasValue)
            {
                query = query.Where(p => p.NgayNhap >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(p => p.NgayNhap <= toDate.Value.AddDays(1));
            }

            return View(await query.OrderByDescending(p => p.NgayNhap).ToListAsync());
        }

        // 2. Mở trang tạo mới (GET)
        [HttpGet]
        public IActionResult Create()
        {
            // Lấy tất cả danh sách Nhà Cung Cấp trong Database truyền sang View
            // (Bỏ điều kiện .Where để tránh bị lọc nhầm trường TT)
            ViewBag.DsNhaCungCap = _context.NhaCCs.ToList();

            return View();
        }

        // 3. Xử lý tạo mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuNhap phieuNhap)
        {
            try
            {
                if (phieuNhap.TT == null) phieuNhap.TT = true;

                _context.PhieuNhaps.Add(phieuNhap);
                await _context.SaveChangesAsync();

                // Đặt thông báo thành công
                TempData["SuccessMessage"] = "Thêm phiếu nhập mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi lưu dữ liệu: " + ex.Message;
                ViewBag.DsNhaCungCap = _context.NhaCCs.ToList();
                return View(phieuNhap);
            }
        }

        // 4. Mở trang sửa (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);
            if (phieuNhap == null) return NotFound();

            // Lấy toàn bộ danh sách NCC truyền sang View
            ViewBag.DsNhaCungCap = await _context.NhaCCs.ToListAsync();

            return View(phieuNhap);
        }

        // 5. Xử lý cập nhật (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PhieuNhap phieuNhap)
        {
            try
            {
                _context.Update(phieuNhap);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật phiếu nhập thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi cập nhật: " + ex.Message;
                ViewBag.DsNhaCungCap = await _context.NhaCCs.ToListAsync();
                return View(phieuNhap);
            }
        }

        // 6. Xử lý Xóa (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);
            if (phieuNhap != null)
            {
                _context.PhieuNhaps.Remove(phieuNhap);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Đã xóa phiếu nhập thành công!" });
            }
            return Json(new { success = false, message = "Không tìm thấy phiếu nhập!" });
        }
        // GET:Details
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            // Lấy phiếu nhập cùng danh sách chi tiết và thông tin nhà cung cấp
            var phieuNhap = await _context.PhieuNhaps
                .FirstOrDefaultAsync(p => p.MaPN == id);

            if (phieuNhap == null) return NotFound();

            // Lấy danh sách chi tiết của phiếu nhập này
            var chiTietList = await _context.CTPhieuNhaps
                .Where(ct => ct.MaPN == id)
                .ToListAsync();

            ViewBag.ChiTietList = chiTietList;

            // Lấy tên Nhà Cung Cấp
            var ncc = await _context.NhaCCs.FirstOrDefaultAsync(n => n.MaNCC == phieuNhap.MaNCC);
            ViewBag.TenNCC = ncc?.TenNCC ?? phieuNhap.MaNCC;

            return View(phieuNhap);
        }
    }
}