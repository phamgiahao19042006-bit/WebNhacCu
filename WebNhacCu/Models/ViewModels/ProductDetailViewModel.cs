using WebNhacCu.Models.DTO;

namespace WebNhacCu.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public ProductDTO? Product { get; set; }

        public List<ProductDTO> RelatedProducts { get; set; } = new();
    }
}