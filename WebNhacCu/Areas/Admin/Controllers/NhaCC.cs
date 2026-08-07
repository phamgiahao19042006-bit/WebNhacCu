using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhaCCController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public NhaCCController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. Hiển thị danh sách
        public async Task<IActionResult> Index(string search, bool? trangThai)
        {
            var query = _context.NhaCCs.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.MaNCC.Contains(search) || n.TenNCC.Contains(search) || n.SDT.Contains(search));
            }

            if (trangThai.HasValue)
            {
                query = query.Where(n => n.TT == trangThai.Value);
            }

            return View(await query.ToListAsync());
        }

        // 2. Mở trang tạo mới (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Xử lý lưu thông tin nhà cung cấp (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaCC nhaCC)
        {
            try
            {
                // Gán mặc định nếu TT bị null
                if (nhaCC.TT == null) nhaCC.TT = true;

                _context.NhaCCs.Add(nhaCC);
                await _context.SaveChangesAsync();

                // Gửi thông báo thành công
                TempData["SuccessMessage"] = "Thêm nhà cung cấp mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Nếu trùng Mã NCC hoặc lỗi DB sẽ bắt ở đây
                ViewBag.Error = "Lỗi khi lưu dữ liệu: " + ex.Message;
                return View(nhaCC);
            }
        }
        // 4. Mở trang Sửa (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var nhaCC = await _context.NhaCCs.FindAsync(id);
            if (nhaCC == null) return NotFound();

            return View(nhaCC);
        }

        // 5. Xử lý Cập nhật (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NhaCC nhaCC)
        {
            try
            {
                _context.Update(nhaCC);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật nhà cung cấp thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi cập nhật: " + ex.Message;
                return View(nhaCC);
            }
        }

        // 6. Xử lý Xóa (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var nhaCC = await _context.NhaCCs.FindAsync(id);
            if (nhaCC != null)
            {
                _context.NhaCCs.Remove(nhaCC);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Đã xóa nhà cung cấp thành công!" });
            }
            return Json(new { success = false, message = "Không tìm thấy nhà cung cấp!" });
        }
    }
}