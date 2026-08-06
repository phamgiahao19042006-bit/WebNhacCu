using Microsoft.AspNetCore.Mvc;

namespace WebNhacCu.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
