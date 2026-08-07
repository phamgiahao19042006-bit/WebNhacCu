using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public CategoryService(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        public async Task<List<LoaiSP>> GetAllAsync()
        {
            return await _context.LoaiSPs
                .Where(x => x.TT)
                .OrderBy(x => x.TenLoai)
                .ToListAsync();
        }

        public async Task<LoaiSP?> GetByIdAsync(string maLoai)
        {
            return await _context.LoaiSPs
                .FirstOrDefaultAsync(x => x.MaLoai == maLoai);
        }
    }
}