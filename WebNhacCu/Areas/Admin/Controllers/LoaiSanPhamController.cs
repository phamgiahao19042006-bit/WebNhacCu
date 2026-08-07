using CoreDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // 1. TRANG DANH SÁCH
        public async Task<IActionResult> Index(string searchString, string statusFilter)
        {
            ViewBag.SearchString = searchString;
            ViewBag.StatusFilter = statusFilter;

            var query = _context.LoaiSPs.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.TenLoai.Contains(searchString) || x.MaLoai.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool isStatus = bool.Parse(statusFilter);
                query = query.Where(x => x.TT == isStatus);
            }

            var list = await query.ToListAsync();
            return View(list);
        }

        // 2. THÊM MỚI (GET + POST AJAX)
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,TT")] LoaiSP loaiSP)
        {
            ModelState.Remove("SanPhams");

            if (_context.LoaiSPs.Any(e => e.MaLoai == loaiSP.MaLoai))
            {
                return BadRequest("Mã loại sản phẩm này đã tồn tại trong hệ thống!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(loaiSP);
                await _context.SaveChangesAsync();
                return Ok(); // AJAX nhận được status 200 OK
            }
            return BadRequest("Dữ liệu không hợp lệ!");
        }

        // 3. SỬA (GET + POST AJAX)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null) return NotFound();
            return View(loaiSP);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLoai,TenLoai,TT")] LoaiSP loaiSP)
        {
            // Nếu id rỗng hoặc không trùng với MaLoai trong form
            if (string.IsNullOrEmpty(id) || id != loaiSP.MaLoai)
            {
                return BadRequest("Mã loại sản phẩm không hợp lệ!");
            }

            ModelState.Remove("SanPhams");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSP);
                    await _context.SaveChangesAsync();
                    return Ok(); // AJAX nhận được status 200 OK
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LoaiSPs.Any(e => e.MaLoai == loaiSP.MaLoai)) return NotFound();
                    else throw;
                }
            }
            return BadRequest("Dữ liệu nhập vào chưa đúng định dạng!");
        }

        // 4. XÓA (POST AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Không tìm thấy mã cần xóa!");

            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null) return NotFound("Loại sản phẩm không tồn tại!");

            try
            {
                _context.LoaiSPs.Remove(loaiSP);
                await _context.SaveChangesAsync();
                return Ok(); // Xóa thành công
            }
            catch (Exception)
            {
                return BadRequest("Không thể xóa loại sản phẩm này vì đang chứa danh mục sản phẩm con!");
            }
        }
    }
}