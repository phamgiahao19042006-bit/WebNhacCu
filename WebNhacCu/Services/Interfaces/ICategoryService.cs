using CoreDatabase.Models;

namespace WebNhacCu.Interfaces
{
    public interface ICategoryService
    {
        Task<List<LoaiSP>> GetAllAsync();

        Task<LoaiSP?> GetByIdAsync(string maLoai);
    }
}