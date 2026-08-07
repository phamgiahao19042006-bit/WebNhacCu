using Microsoft.AspNetCore.Mvc;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.ViewModels;
using WebNhacCu.Services;

namespace WebNhacCu.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductViewModel
            {
                Products = await _productService.GetAllAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = await _productService.GetByCategoryAsync(product.MaLoai)
            };

            return View(viewModel);
        }
    }
}