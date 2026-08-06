using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
