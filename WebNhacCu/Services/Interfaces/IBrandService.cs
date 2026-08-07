using CoreDatabase.Models;

namespace WebNhacCu.Interfaces
{
    public interface IBrandService
    {
        Task<List<ThuongHieu>> GetAllAsync();

        Task<ThuongHieu?> GetByIdAsync(string maTH);
    }
}