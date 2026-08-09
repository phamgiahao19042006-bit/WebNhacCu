using CoreDatabase.Models; // Kiem tra namespace theo dung du an cua Hao
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachHangController : Controller
    {
        private readonly WebHeThongBanNhacCuContext _context; // Thay YourDbContext bang DbContext cua du an

        public KhachHangController(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        // GET: Admin/KhachHang
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Dùng SQL chuẩn hóa tất cả các cột có nguy cơ bị NULL về giá trị mặc định 
            // trước khi EF Core gán dữ liệu vào Model KhachHang
            var list = await _context.KhachHangs.FromSqlRaw(@"
        SELECT 
            MaKH,
            ISNULL(TenDangNhap, '') AS TenDangNhap,
            ISNULL(MatKhau, '') AS MatKhau,
            ISNULL(HoTen, N'') AS HoTen,
            ISNULL(SDT, '') AS SDT,
            ISNULL(Email, '') AS Email,
            ISNULL(DiaChi, N'') AS DiaChi,
            ISNULL(DiemTichLuy, 0) AS DiemTichLuy,
            ISNULL(TT, 1) AS TT,
            ISNULL(CreatedDate, GETDATE()) AS CreatedDate,
            ISNULL(UpdatedDate, GETDATE()) AS UpdatedDate,
            ISNULL(CreatedBy, '') AS CreatedBy,
            ISNULL(UpdatedBy, '') AS UpdatedBy
        FROM KhachHang
    ").ToListAsync();

            return View(list);
        }

        // POST: Admin/KhachHang/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(KhachHang khachHang)
        {
            try
            {
                // Tim xem khach hang da ton tai trong CSDL chua
                var existing = await _context.KhachHangs.FindAsync(khachHang.MaKH);

                if (existing != null)
                {
                    // CASE 1: NEU DA TON TAI -> CAP NHAT
                    existing.HoTen = khachHang.HoTen;
                    existing.SDT = khachHang.SDT;
                    existing.Email = khachHang.Email;
                    existing.DiaChi = khachHang.DiaChi;
                    existing.DiemTichLuy = khachHang.DiemTichLuy;
                    existing.TT = khachHang.TT;

                    _context.Entry(existing).State = EntityState.Modified;
                    TempData["SuccessMessage"] = "Cập nhật thông tin khách hàng thành công!";
                }
                else
                {
                    // CASE 2: NEU CHUA TON TAI -> THEM MOI
                    _context.KhachHangs.Add(khachHang);
                    TempData["SuccessMessage"] = "Thêm mới khách hàng thành công!";
                }

                // Luu thay doi xuong SQL Server
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi lưu dữ liệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/KhachHang/Delete/KH01
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa khách hàng thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}