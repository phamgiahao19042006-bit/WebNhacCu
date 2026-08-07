using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDatabase.Models;
using WebNhacCu.Models.EF; // Sửa tên Namespace chứa DbContext của Hào nếu cần

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Thay YourDbContext bằng tên DbContext của dự án

        public NhanVienController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH & FORM TẠO/SỬA
        [HttpGet]
        public async Task<IActionResult> Index(string editId)
        {
            var list = await _context.NhanViens.ToListAsync();

            NhanVien model = new NhanVien
            {
                MaNV = "NV" + DateTime.Now.ToString("yyMMddHHmmss"),
                NgaySinh = DateTime.Now.AddYears(-18),
                GioiTinh = "Nam",
                TT = true
            };

            // Nếu bấm Sửa -> Lấy dữ liệu lên Form
            if (!string.IsNullOrEmpty(editId))
            {
                var nv = await _context.NhanViens.FindAsync(editId);
                if (nv != null)
                {
                    model = nv;
                }
            }

            ViewBag.NhanViens = list;
            return View(model);
        }

        // 2. LƯU (THÊM MỚI HOẶC CẬP NHẬT)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(NhanVien nhanVien)
        {
            try
            {
                var existing = await _context.NhanViens.FindAsync(nhanVien.MaNV);
                if (existing == null)
                {
                    _context.NhanViens.Add(nhanVien);
                    TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                }
                else
                {
                    existing.HoTen = nhanVien.HoTen;
                    existing.NgaySinh = nhanVien.NgaySinh;
                    existing.GioiTinh = nhanVien.GioiTinh;
                    existing.SDT = nhanVien.SDT;
                    existing.Email = nhanVien.Email;
                    existing.DiaChi = nhanVien.DiaChi;
                    existing.TT = nhanVien.TT;

                    _context.Entry(existing).State = EntityState.Modified;
                    TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi lưu dữ liệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // 3. XÓA NHÂN VIÊN
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv != null)
            {
                _context.NhanViens.Remove(nv);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Xóa nhân viên thành công!" });
            }
            return Json(new { success = false, message = "Không tìm thấy nhân viên!" });
        }
    }
}