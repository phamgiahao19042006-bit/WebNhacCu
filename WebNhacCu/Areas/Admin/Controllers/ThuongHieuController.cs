using CoreDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Models.EF; 

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuongHieuController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; 

        public ThuongHieuController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH THƯƠNG HIỆU
        public async Task<IActionResult> Index()
        {
            var list = await _context.ThuongHieus
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
            return View(list);
        }

        // 2. THÊM MỚI - GET
        public IActionResult Create()
        {
            return View();
        }

        // 2. THÊM MỚI - POST
        [HttpPost]
        public async Task<IActionResult> Create(ThuongHieu thuongHieu)
        {
            ModelState.Remove("SanPhams");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("UpdatedBy");

            if (string.IsNullOrWhiteSpace(thuongHieu.MaTH))
                ModelState.AddModelError("MaTH", "Mã thương hiệu không được để trống!");

            if (string.IsNullOrWhiteSpace(thuongHieu.TenTH))
                ModelState.AddModelError("TenTH", "Tên thương hiệu không được để trống!");

            if (ModelState.IsValid)
            {
                try
                {
                    thuongHieu.CreatedDate = DateTime.Now;
                    _context.ThuongHieus.Add(thuongHieu);
                    await _context.SaveChangesAsync();

                    // Đặt cờ báo thành công để View kích hoạt SweetAlert
                    ViewBag.IsSuccess = true;
                    return View(thuongHieu);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi CSDL: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }

            return View(thuongHieu);
        }

        // 3. CHỈNH SỬA - GET
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var thuongHieu = await _context.ThuongHieus.FindAsync(id);
            if (thuongHieu == null) return NotFound();

            return View(thuongHieu);
        }

        // 3. CHỈNH SỬA - POST
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.MaTH) return NotFound();

            // Loại bỏ kiểm tra không cần thiết
            ModelState.Remove("SanPhams");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("UpdatedBy");

            if (string.IsNullOrWhiteSpace(thuongHieu.TenTH))
            {
                ModelState.AddModelError("TenTH", "Tên thương hiệu không được để trống!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    thuongHieu.UpdatedDate = DateTime.Now;

                    // Cập nhật thông tin vào DB
                    _context.Update(thuongHieu);
                    await _context.SaveChangesAsync();

                    // Đặt cờ báo thành công để View hiển thị SweetAlert2
                    ViewBag.IsSuccess = true;
                    return View(thuongHieu);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi CSDL: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }

            return View(thuongHieu);
        }

        // 4. XÓA THƯƠNG HIỆU - POST
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Mã thương hiệu không hợp lệ!" });
            }

            try
            {
                var thuongHieu = await _context.ThuongHieus.FindAsync(id);
                if (thuongHieu != null)
                {
                    _context.ThuongHieus.Remove(thuongHieu);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Xóa thương hiệu thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy thương hiệu để xóa!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Không thể xóa vì thương hiệu này đang có sản phẩm liên kết!" });
            }
        }
    }
}