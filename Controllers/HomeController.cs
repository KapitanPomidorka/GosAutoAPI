using Microsoft.AspNetCore.Mvc;

namespace GosAutoAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
