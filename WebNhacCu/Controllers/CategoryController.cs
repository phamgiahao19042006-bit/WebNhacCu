using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
