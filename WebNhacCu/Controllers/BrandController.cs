using Microsoft.AspNetCore.Mvc;
using WebNhacCu.Interfaces;

namespace WebNhacCu.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }
    }
}
