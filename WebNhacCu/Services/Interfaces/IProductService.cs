using WebNhacCu.Models.DTO;

namespace WebNhacCu.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();

        Task<ProductDTO?> GetByIdAsync(string maSP);

        Task<List<ProductDTO>> SearchAsync(string keyword);

        Task<List<ProductDTO>> GetByCategoryAsync(string maLoai);

        Task<List<ProductDTO>> GetByBrandAsync(string maTH);

        Task<List<ProductDTO>> GetNewestAsync(int quantity);
    }
}