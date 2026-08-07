using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.DTO;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Services
{
    public class ProductService : IProductService
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public ProductService(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        private static ProductDTO MapToDTO(SanPham sp)
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



        public async Task<(List<ProductDTO> Items, int TotalItems)> GetProductsAsync(ProductQueryDTO query)
        {
            query.Page = Math.Max(query.Page, 1);
            query.PageSize = Math.Max(query.PageSize, 1);
            var products = _context.SanPhams
                .AsNoTracking()
                .Include(x => x.LoaiSP)
                .Include(x => x.ThuongHieu)
                .Where(x => x.TT);

            var totalItems = await products.CountAsync();

            var items = await products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (
                items.Select(MapToDTO).ToList(),
                totalItems
            );
        }
        public async Task<ProductDTO?> GetByIdAsync(string maSP)
        {
            var product = await _context.SanPhams
                .AsNoTracking()
                .Include(x => x.LoaiSP)
                .Include(x => x.ThuongHieu)
                .FirstOrDefaultAsync(x => x.MaSP == maSP);

            return product == null ? null : MapToDTO(product);
        }

        public async Task<List<ProductDTO>> GetNewestAsync(int count)
        {
            return await _context.SanPhams
                .AsNoTracking()
                .Include(x => x.LoaiSP)
                .Include(x => x.ThuongHieu)
                .Where(x => x.TT)
                .OrderByDescending(x => x.CreatedDate)
                .Take(count)
                .Select(x => MapToDTO(x))
                .ToListAsync();
        }

        public async Task<List<ProductDTO>> GetFeaturedAsync(int count)
        {
            return await _context.SanPhams
                .AsNoTracking()
                .Include(x => x.LoaiSP)
                .Include(x => x.ThuongHieu)
                .Where(x => x.TT)
                .OrderByDescending(x => x.SoLuongTon)
                .Take(count)
                .Select(x => MapToDTO(x))
                .ToListAsync();
        }

        public async Task<List<ProductDTO>> GetRelatedAsync(string maSP)
        {
            var product = await _context.SanPhams
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MaSP == maSP);

            if (product == null)
                return new List<ProductDTO>();

            return await _context.SanPhams
                .AsNoTracking()
                .Include(x => x.LoaiSP)
                .Include(x => x.ThuongHieu)
                .Where(x => x.MaLoai == product.MaLoai
                         && x.MaSP != maSP
                         && x.TT)
                .Take(4)
                .Select(x => MapToDTO(x))
                .ToListAsync();
        }
    }
}