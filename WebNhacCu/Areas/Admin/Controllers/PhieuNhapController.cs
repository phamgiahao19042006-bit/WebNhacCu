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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dsPhieuNhap = await _context.PhieuNhaps
                                            .OrderByDescending(p => p.NgayNhap)
                                            .ToListAsync();

            return View(dsPhieuNhap);
        }

        // 2. Mở trang tạo mới (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Lấy tất cả danh sách Nhà Cung Cấp trong Database truyền sang View
            // (Bỏ điều kiện .Where để tránh bị lọc nhầm trường TT)
            ViewBag.NhaCungCapList = await _context.NhaCCs.ToListAsync();
            ViewBag.SanPhamList = await _context.SanPhams.ToListAsync();
            return View();
        }

        // 3. Xử lý tạo mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuNhap phieuNhap, List<CTPhieuNhap> CTPhieuNhaps)
        {
            // Bỏ qua validate các navigation properties tự động của Entity Framework
            ModelState.Remove("MaNCCNavigation");
            ModelState.Remove("ChiTietPhieuNhaps");

            try
            {
                // 1. Gán ngày tạo nếu trống
                if (phieuNhap.NgayNhap == default)
                {
                    phieuNhap.NgayNhap = DateTime.Now;
                }

                // 2. Thêm Phiếu nhập chính vào DB
                _context.PhieuNhaps.Add(phieuNhap);
                await _context.SaveChangesAsync(); // Lưu để phát sinh/xác nhận MaPN

                // 3. Thêm danh sách Chi tiết phiếu nhập
                if (CTPhieuNhaps != null && CTPhieuNhaps.Count > 0)
                {
                    foreach (var item in CTPhieuNhaps)
                    {
                        item.MaPN = phieuNhap.MaPN; // Khóa ngoại kết nối với Phiếu nhập
                        _context.CTPhieuNhaps.Add(item);
                    }
                    await _context.SaveChangesAsync(); // Lưu chi tiết
                }

                // 4. Lưu xong -> Chuyển về trang danh sách NhapHang/PhieuNhap
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Nếu có lỗi thì load lại danh sách dropdown và báo lỗi
                ViewBag.NhaCungCapList = await _context.NhaCCs.ToListAsync();
                ViewBag.SanPhamList = await _context.SanPhams.ToListAsync();
                ModelState.AddModelError("", "Lỗi lưu phiếu nhập: " + ex.Message);
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