using CoreDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Models.EF; 

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public SanPhamController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH SẢN PHẨM (INDEX)
        public async Task<IActionResult> Index()
        {
            var sanPhams = await _context.SanPhams
                .Include(s => s.LoaiSP) // Load tên Loại Sản Phẩm
                .Include(s => s.ThuongHieu) // Load tên Thương Hiệu (nếu có)
                .OrderByDescending(s => s.CreatedDate)
                .ToListAsync();

            return View(sanPhams);
        }

        // 2. THÊM MỚI SẢN PHẨM - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Lấy danh sách Loại SP và Thương hiệu từ Database
            ViewBag.MaLoai = new SelectList(from l in _context.LoaiSPs select l, "MaLoai", "TenLoai");
            ViewBag.MaTH = new SelectList(from t in _context.ThuongHieus select t, "MaTH", "TenTH");

            return View();
        }

        // 2. THÊM SẢN PHẨM - POST (Lưu vào Database)
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile? HinhAnhFile)
        {
            // Loại bỏ validation cho các thuộc tính navigation để không dính lỗi ModelState
            ModelState.Remove("MaLoaiNavigation");
            ModelState.Remove("MaTHNavigation");
            ModelState.Remove("LoaiSanPham");
            ModelState.Remove("ThuongHieu");

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý Upload Ảnh
                    if (HinhAnhFile != null && HinhAnhFile.Length > 0)
                    {
                        // 1. Xác định đường dẫn thư mục lưu ảnh
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sanpham");

                        // 2. TỰ ĐỘNG TẠO THƯ MỤC NẾU CHƯA TỒN TẠI (Giúp tránh lỗi 100%)
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // 3. Tạo tên file duy nhất
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnhFile.FileName);
                        var filePath = Path.Combine(folderPath, fileName);

                        // 4. Lưu file vào thư mục
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnhFile.CopyToAsync(stream);
                        }

                        sanPham.HinhAnh = fileName;
                    }

                    sanPham.CreatedDate = DateTime.Now;
                    _context.SanPhams.Add(sanPham);
                    await _context.SaveChangesAsync();

                    ViewBag.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi CSDL: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }

            // Load lại danh sách Dropdown
            ViewBag.MaLoai = new SelectList((from l in _context.LoaiSPs select l).ToList(), "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaTH = new SelectList((from t in _context.ThuongHieus select t).ToList(), "MaTH", "TenTH", sanPham.MaTH);

            return View(sanPham);
        }

        // 3. CHỈNH SỬA SẢN PHẨM - GET
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null) return NotFound();

            ViewBag.MaLoai = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaTH = new SelectList(_context.ThuongHieus, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // 3. CHỈNH SỬA SẢN PHẨM - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SanPham sanPham, IFormFile? HinhAnhFile)
        {
            if (id != sanPham.MaSP) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu chọn ảnh mới thì cập nhật, không thì giữ nguyên ảnh cũ
                    if (HinhAnhFile != null && HinhAnhFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnhFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sanpham", fileName);

                        var directory = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnhFile.CopyToAsync(stream);
                        }
                        sanPham.HinhAnh = "/images/sanpham/" + fileName;
                    }

                    sanPham.UpdatedDate = DateTime.Now;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SanPhams.Any(e => e.MaSP == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoai = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaTH = new SelectList(_context.ThuongHieus, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // 4. XÓA SẢN PHẨM
        
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Mã sản phẩm không hợp lệ!" });
            }

            try
            {
                var sanPham = await _context.SanPhams.FindAsync(id);
                if (sanPham == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong CSDL!" });
                }

                // Xóa sản phẩm khỏi DB
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xóa: " + ex.Message });
            }
        }
    }
}