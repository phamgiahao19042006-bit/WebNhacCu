using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF; // Kiem tra lai Namespace DbContext cua Hao

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Sua YourDbContext thanh ten DbContext cua du an

        public HoaDonController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. CHỨC NĂNG: LẬP HÓA ĐƠN
        // URL: /Admin/HoaDon/LapHoaDon
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> LapHoaDon()
        {
            ViewBag.KhachHang = await _context.KhachHangs.ToListAsync();
            ViewBag.NhanVien = await _context.NhanViens.ToListAsync();

            var model = new HoaDon
            {
                
                NgayLap = DateTime.Now,
                TongTien = 0,
                GiamGia = 0,
                ThanhTien = 0,
                TT = true
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult LapHoaDon(HoaDon model)
        {
            if (ModelState.IsValid)
            {
                // 1. Lưu hóa đơn vào Database
                _context.HoaDons.Add(model);
                _context.SaveChanges();

                // 2. Bắn thông báo thành công qua TempData
                TempData["SuccessMessage"] = "Lập hóa đơn thành công!";

                // 3. Quay lại trang danh sách Hóa Đơn (Hoặc trang Lập hóa đơn)
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Danh sách hóa đơn (Menu "Danh sách hóa đơn")
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _context.HoaDons.OrderByDescending(h => h.NgayLap).ToListAsync();
            return View(list);
        }
        // 1. Hàm Xóa
        public IActionResult Delete(string id)
        {
            var hoaDon = _context.HoaDons.Find(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đã xóa hóa đơn thành công!";
            }
            return RedirectToAction("Index");
        }

        // 2. Hàm Cập nhật l
        // 1. GET: Lấy thông tin Hóa đơn truyền sang trang Edit.cshtml
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var hoaDon = _context.HoaDons.Find(id);
            if (hoaDon == null) return NotFound();

            return View(hoaDon);
        }

        // 2. POST: Nhận dữ liệu sau khi sửa từ trang Edit.cshtml
        [HttpPost]
        public IActionResult Edit(HoaDon model)
        {
            var hoaDon = _context.HoaDons.Find(model.MaHD);
            if (hoaDon != null)
            {
                hoaDon.MaKH = model.MaKH;
                hoaDon.MaNV = model.MaNV;
                hoaDon.TongTien = model.TongTien;
                hoaDon.GiamGia = model.GiamGia;
                hoaDon.ThanhTien = model.TongTien - model.GiamGia;
                hoaDon.PhuongThucTT = model.PhuongThucTT;
                hoaDon.TT = model.TT;

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật hóa đơn thành công!";
            }

            return RedirectToAction("Index");
        }
    }
}