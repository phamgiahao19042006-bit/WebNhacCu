using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.DTO;
using WebNhacCu.Models.EF;
using WebNhacCu.Services.Interfaces;

namespace WebNhacCu.Services
{
    public class ProductService : IProductService
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public ProductService(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .Where(p => p.TT)
                .OrderBy(p => p.TenSP)
                .ToListAsync();

            return products.Select(ToDTO).ToList();
        }

        public async Task<ProductDTO?> GetByIdAsync(string maSP)
        {
            var product = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .FirstOrDefaultAsync(p => p.MaSP == maSP && p.TT);

            if (product == null)
                return null;

            return ToDTO(product);
        }

        public async Task<List<ProductDTO>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim();

            var products = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .Where(p => p.TT &&
                            p.TenSP.Contains(keyword))
                .ToListAsync();

            return products.Select(ToDTO).ToList();
        }

        public async Task<List<ProductDTO>> GetByCategoryAsync(string maLoai)
        {
            var products = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .Where(p => p.MaLoai == maLoai && p.TT)
                .ToListAsync();

            return products.Select(ToDTO).ToList();
        }

        public async Task<List<ProductDTO>> GetByBrandAsync(string maTH)
        {
            var products = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .Where(p => p.MaTH == maTH && p.TT)
                .ToListAsync();

            return products.Select(ToDTO).ToList();
        }

        public async Task<List<ProductDTO>> GetNewestAsync(int quantity)
        {
            var products = await _context.SanPhams
                .Include(p => p.LoaiSP)
                .Include(p => p.ThuongHieu)
                .Where(p => p.TT)
                .OrderByDescending(p => p.CreatedDate)
                .Take(quantity)
                .ToListAsync();

            return products.Select(ToDTO).ToList();
        }

        private static ProductDTO ToDTO(SanPham sp)
        {
            return new ProductDTO
            {
                MaSP = sp.MaSP,
                TenSP = sp.TenSP,
                DonGia = sp.DonGia,
                SoLuongTon = sp.SoLuongTon,
                HinhAnh = sp.HinhAnh,
                MoTa = sp.MoTa,
                TT = sp.TT,

                MaLoai = sp.MaLoai,
                TenLoai = sp.LoaiSP?.TenLoai ?? string.Empty,

                MaTH = sp.MaTH,
                TenTH = sp.ThuongHieu?.TenTH ?? string.Empty
            };
        }
    }
}