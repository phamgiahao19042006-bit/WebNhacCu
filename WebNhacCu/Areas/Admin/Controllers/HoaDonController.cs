using CoreDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Models.EF; // Kiem tra lai Namespace DbContext cua Hao
using WebNhacCu.Models.ViewModels;

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
        // 1. GET: Admin/HoaDon/LapHoaDon
        // 1. GET: Admin/HoaDon/LapHoaDon
        [HttpGet]
        public async Task<IActionResult> LapHoaDon()
        {
            ViewBag.KhachHang = await _context.KhachHangs
                .Select(k => new KhachHang { MaKH = k.MaKH, HoTen = k.HoTen })
                .ToListAsync();

            ViewBag.NhanVien = await _context.NhanViens.ToListAsync();

            ViewBag.SanPham = await _context.SanPhams
                .Select(s => new { s.MaSP, s.TenSP, s.DonGia })
                .ToListAsync();

            // 📌 Thêm khởi tạo model mới tại đây để truyền sang View
            var model = new HoaDon
            {
                NgayLap = DateTime.Now,
                TongTien = 0,
                GiamGia = 0,
                ThanhTien = 0,
                TT = true
            };

            return View(model); // 👈 Truyền model vào đây
        }

        // 2. POST: Admin/HoaDon/LapHoaDon (Lưu Hóa Đơn + Chi Tiết Hóa Đơn)
        [HttpPost]
        public async Task<IActionResult> LapHoaDon([FromBody] LapHoaDonViewModel model)
        {
            // Bỏ qua bước check null cứng này để kiểm tra xem data vào chưa, 
            // hoặc kiểm tra đúng biến ChiTiet
            if (model == null || model.ChiTiet == null || model.ChiTiet.Count == 0)
            {
                // Ghi rõ để debug nếu cần
                return Json(new { success = false, message = "Vui lòng chọn ít nhất 1 sản phẩm!" });
            }

            try
            {
                // 1. Tạo hóa đơn mới
                var hoaDon = new HoaDon
                {
                    MaHD = model.MaHD,
                    NgayLap = model.NgayLap,
                    MaKH = model.MaKH,
                    MaNV = model.MaNV,
                    PhuongThucTT = model.PhuongThucTT,
                    GiamGia = model.GiamGia,
                    TongTien = model.ChiTiet.Sum(x => x.DonGia * x.SoLuong),
                    ThanhTien = model.ChiTiet.Sum(x => x.DonGia * x.SoLuong) - model.GiamGia,
                    TT = model.TrangThai == 1
                };

                _context.HoaDons.Add(hoaDon);

                // 2. Lưu danh sách Chi tiết hóa đơn
                foreach (var item in model.ChiTiet)
                {
                    var ct = new CTHoaDon
                    {
                        MaHD = model.MaHD,
                        MaSP = item.MaSP,
                        DonGia = item.DonGia,
                        SoLuong = item.SoLuong,
                        
                    };
                    _context.CTHoaDons.Add(ct);
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Lập hóa đơn thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi lưu dữ liệu: " + ex.Message });
            }
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