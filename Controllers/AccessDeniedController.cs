using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDatabase.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
