using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
