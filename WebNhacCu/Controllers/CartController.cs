using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
