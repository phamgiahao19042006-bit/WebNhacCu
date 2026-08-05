using CoreDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiSanPhamController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public LoaiSanPhamController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH + TÌM KIẾM + LỌC
        public async Task<IActionResult> Index(string searchString, string statusFilter)
        {
            var query = _context.LoaiSPs.AsQueryable();

            // Tìm kiếm theo Mã loại hoặc Tên loại
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                query = query.Where(l => l.MaLoai.ToLower().Contains(searchString) ||
                                         l.TenLoai.ToLower().Contains(searchString));
            }

            // Lọc theo Trạng thái (true: Đang kinh doanh, false: Dừng kinh doanh)
            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (bool.TryParse(statusFilter, out bool isActived))
                {
                    query = query.Where(l => l.TT == isActived);
                }
            }

            ViewBag.SearchString = searchString;
            ViewBag.StatusFilter = statusFilter;

            var result = await query.ToListAsync();
            return View(result);
        }

        // 2. THÊM LOẠI SẢN PHẨM (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 2. THÊM LOẠI SẢN PHẨM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                // Tự động sinh Mã Loại nếu chưa có (Ví dụ: L001, L002...)
                if (string.IsNullOrEmpty(loaiSP.MaLoai))
                {
                    var count = await _context.LoaiSPs.CountAsync() + 1;
                    loaiSP.MaLoai = "L" + count.ToString("D3");
                }

                loaiSP.TT = true; // Mặc định khi thêm mới là Đang kinh doanh
                _context.Add(loaiSP);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm loại sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSP);
        }

        // 3. CẬP NHẬT LOẠI SẢN PHẨM (GET)
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null) return NotFound();

            return View(loaiSP);
        }

        // 3. CẬP NHẬT LOẠI SẢN PHẨM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LoaiSP loaiSP)
        {
            if (id != loaiSP.MaLoai) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSP);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật loại sản phẩm thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LoaiSPs.Any(e => e.MaLoai == loaiSP.MaLoai))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSP);
        }

        // 4. XÓA MỀM (Chuyển trạng thái sang Dừng kinh doanh)
        [HttpPost]
        public async Task<IActionResult> DeleteSoft(string id)
        {
            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP != null)
            {
                loaiSP.TT = false; // Chuyển trạng thái thành dừng kinh doanh
                _context.Update(loaiSP);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã chuyển loại sản phẩm '{loaiSP.TenLoai}' sang Dừng kinh doanh!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}