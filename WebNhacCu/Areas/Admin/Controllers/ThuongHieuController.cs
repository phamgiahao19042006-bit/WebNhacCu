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

        // 3. CHỈNH SỬA 

        // GET: Admin/ThuongHieu/Edit/TH010
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var thuongHieu = await _context.ThuongHieus.FindAsync(id);
            if (thuongHieu == null) return NotFound();

            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieu/Edit/TH010
        [HttpPost]
        public async Task<IActionResult> Edit(ThuongHieu model)
        {
            try
            {
                var thInDb = await _context.ThuongHieus.FindAsync(model.MaTH);

                if (thInDb != null)
                {
                    thInDb.TenTH = model.TenTH;
                    thInDb.QuocGia = model.QuocGia;

                    _context.ThuongHieus.Update(thInDb);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "ThuongHieu", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi lưu: " + ex.Message);
                return View(model);
            }
        }

        // 4. XÓA THƯƠNG HIỆU - POST
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var thuongHieu = await _context.ThuongHieus.FindAsync(id);
            if (thuongHieu != null)
            {
                _context.ThuongHieus.Remove(thuongHieu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa thương hiệu thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}