using WebNhacCu.Models.DTO;

namespace WebNhacCu.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO?> GetByIdAsync(string maSP);
        Task<List<ProductDTO>> GetNewestAsync(int count);
        Task<List<ProductDTO>> GetFeaturedAsync(int count);
        Task<List<ProductDTO>> GetRelatedAsync(string maSP);
        Task<(List<ProductDTO> Items, int TotalItems)> GetProductsAsync(ProductQueryDTO query);
    }
}