using Microsoft.AspNetCore.Mvc;

namespace KmouHelmet.Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
