using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhuyenMaiController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Đổi tên DbContext đúng với project của Hào

        public KhuyenMaiController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // GET: Admin/KhuyenMai/Create (Hiển thị trang form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhuyenMai/Create (Xử lý lưu vào Database)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhuyenMai km)
        {
            // Bỏ qua validate các trường Audit/Navigation không bắt buộc từ Form
            ModelState.Remove("CTKhuyenMais");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("UpdatedBy");

            if (ModelState.IsValid)
            {
                try
                {
                    // Gán ngày tạo mặc định
                    km.CreatedDate = DateTime.Now;
                    km.UpdatedDate = DateTime.Now;

                    _context.KhuyenMais.Add(km);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Thêm khuyến mãi thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Lỗi lưu dữ liệu: " + ex.Message;
                }
            }
            else
            {
                // Lấy danh sách lỗi ra để debug nếu Form vẫn không hợp lệ
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewBag.Error = "Dữ liệu không hợp lệ: " + string.Join(" | ", errors);
            }

            return View(km);
        }

        // GET: Admin/KhuyenMai/Edit/KM0001
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var km = await _context.KhuyenMais.FindAsync(id);
            if (km == null) return NotFound();

            return View(km);
        }

        // POST: Admin/KhuyenMai/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, KhuyenMai km)
        {
            if (id != km.MaKM) return BadRequest();

            // Bỏ qua validate các trường không cần thiết từ Form
            ModelState.Remove("CTKhuyenMais");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("UpdatedBy");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingKM = await _context.KhuyenMais.FindAsync(id);
                    if (existingKM == null) return NotFound();

                    // Cập nhật các trường thông tin
                    existingKM.TenKhuyenMai = km.TenKhuyenMai;
                    existingKM.LoaiGiam = km.LoaiGiam;
                    existingKM.GiaTriGiam = km.GiaTriGiam;
                    existingKM.NgayBatDau = km.NgayBatDau;
                    existingKM.NgayKetThuc = km.NgayKetThuc;
                    existingKM.DieuKienApDung = km.DieuKienApDung;
                    existingKM.TT = km.TT;
                    existingKM.UpdatedDate = DateTime.Now;

                    _context.Update(existingKM);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Lỗi khi cập nhật: " + ex.Message;
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewBag.Error = "Dữ liệu không hợp lệ: " + string.Join(" | ", errors);
            }

            return View(km);
        }

        // 1. DANH SÁCH KHUYẾN MÃI (INDEX)
        public async Task<IActionResult> Index(string search, DateTime? tuNgay, DateTime? denNgay, string loaiGiam, bool? trangThai)
        {
            var query = _context.KhuyenMais.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(k => k.MaKM.Contains(search) || k.TenKhuyenMai.Contains(search));
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(k => k.NgayBatDau >= tuNgay.Value);
            }

            if (denNgay.HasValue)
            {
                query = query.Where(k => k.NgayKetThuc <= denNgay.Value);
            }

            if (!string.IsNullOrEmpty(loaiGiam))
            {
                query = query.Where(k => k.LoaiGiam == loaiGiam);
            }

            if (trangThai.HasValue)
            {
                query = query.Where(k => k.TT == trangThai.Value);
            }

            return View(await query.OrderByDescending(k => k.NgayBatDau).ToListAsync());
        }

        // 2. CHI TIẾT KHUYẾN MÃI (DETAILS)
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var km = await _context.KhuyenMais
                .FirstOrDefaultAsync(m => m.MaKM == id);

            if (km == null) return NotFound();

            // Lấy danh sách chi tiết khuyến mãi cùng với thông tin sản phẩm
            var listCT = await _context.CTKhuyenMais
                .Include(c => c.SanPham) // Relationship SanPham
                .Where(c => c.MaKM == id)
                .ToListAsync();

            ViewBag.ChiTietList = listCT;

            return View(km);
        }

        // 3. XÓA KHUYẾN MÃI (POST AJAX)
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var km = await _context.KhuyenMais.FindAsync(id);
            if (km == null) return Json(new { success = false, message = "Không tìm thấy chương trình khuyến mãi!" });

            // Xóa chi tiết liên quan trước
            var ctList = _context.CTKhuyenMais.Where(c => c.MaKM == id);
            _context.CTKhuyenMais.RemoveRange(ctList);

            _context.KhuyenMais.Remove(km);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa chương trình khuyến mãi thành công!" });
        }
    }
}