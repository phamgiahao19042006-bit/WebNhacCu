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
            if (ModelState.IsValid)
            {
                _context.KhuyenMais.Add(km);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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