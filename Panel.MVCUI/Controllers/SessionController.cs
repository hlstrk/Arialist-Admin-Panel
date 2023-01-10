using Microsoft.AspNetCore.Mvc;

namespace Panel.MVCUI.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
