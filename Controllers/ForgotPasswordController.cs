using CodeFirstDatabase.Models.Accounts;
using CodeFirstDatabase.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace CodeFirstDatabase.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgotPasswordController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "ResetPassword",
                        new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);

                    // Send the reset link to the user's email
                    // Example: EmailService.SendEmailAsync(user.Email, "Reset Password",
                    //    $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");
                    //SECURE = 587
                    //UNSECURE = 25
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("jayzel.private@gmail.com","tokyooojd0604"),
                        EnableSsl = false
                    };
                    client.Send("jayzel.private@gmail.com", model.Email,"Reset Password", $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                // If user does not exist, don't reveal that to the user for security reasons
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(model);
        }

    }
}
