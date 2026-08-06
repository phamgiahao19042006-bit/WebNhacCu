using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
