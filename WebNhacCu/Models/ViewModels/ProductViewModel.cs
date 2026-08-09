using CoreDatabase.Models;
using WebNhacCu.Models.DTO;

namespace WebNhacCu.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
            = Enumerable.Empty<ProductDTO>();

        public ProductQueryDTO Query { get; set; } = new();

        public IEnumerable<LoaiSP> Categories { get; set; }
            = Enumerable.Empty<LoaiSP>();

        public IEnumerable<ThuongHieu> Brands { get; set; }
            = Enumerable.Empty<ThuongHieu>();

        public int TotalItems { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalItems / Query.PageSize);
    }
}