using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
