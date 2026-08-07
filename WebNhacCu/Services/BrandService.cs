using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Services
{
    public class BrandService : IBrandService
    {
        private readonly WebHeThongBanNhacCuContext _context;

        public BrandService(WebHeThongBanNhacCuContext context)
        {
            _context = context;
        }

        public async Task<List<ThuongHieu>> GetAllAsync()
        {
            return await _context.ThuongHieus
                .Where(x => x.TT)
                .OrderBy(x => x.TenTH)
                .ToListAsync();
        }

        public async Task<ThuongHieu?> GetByIdAsync(string maTH)
        {
            return await _context.ThuongHieus
                .FirstOrDefaultAsync(x => x.MaTH == maTH);
        }
    }
}