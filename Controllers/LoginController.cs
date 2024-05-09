using CodeFirstDatabase.Database;
using CodeFirstDatabase.Models.Accounts;
using CodeFirstDatabase.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace CodeFirstDatabase.Controllers
{
    [AllowAnonymous]
    public class LoginController(SignInManager<ApplicationUser> signManager, CodeFirstDbContext codeFirstDbContext) : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signManager;
        private readonly CodeFirstDbContext _codeFirstDbContext = codeFirstDbContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var uName = model.Username;

                if (IsEmail(model.Username))
                {
                    var uData = _codeFirstDbContext.Users.FirstOrDefault(x => x.Email == model.Username);
                    if (uData != null)
                    {
                        uName = uData.UserName;
                    }
                }

                var res = _signInManager.PasswordSignInAsync(uName!, model.Password,model.RememberMe,false).GetAwaiter().GetResult();
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username/password";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                _signInManager.SignOutAsync().GetAwaiter().GetResult();
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public bool IsEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
