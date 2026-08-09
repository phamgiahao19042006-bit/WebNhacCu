using Microsoft.AspNetCore.Mvc;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.DTO;
using WebNhacCu.Models.ViewModels;

namespace WebNhacCu.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        // Danh sách sản phẩm
        public async Task<IActionResult> Index(ProductQueryDTO query)
        {
            var result = await _productService.GetProductsAsync(query);

            var viewModel = new ProductViewModel
            {
                Products = result.Items,
                TotalItems = result.TotalItems,
                Query = query,
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync()
            };

            return View(viewModel);
        }

        // Chi tiết sản phẩm
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = await _productService.GetRelatedAsync(id)
            };

            return View(viewModel);
        }


    }
}