using CodeFirstDatabase.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDatabase.Controllers
{
    [Authorize(Roles = RoleUtils.Options.RoleSuperOrAdmin)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
