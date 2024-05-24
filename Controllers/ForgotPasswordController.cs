using CodeFirstDatabase.Models.Accounts;
using CodeFirstDatabase.Models.Identity;
using CodeFirstDatabase.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDatabase.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;


        public ForgotPasswordController(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
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
                    //var client = new SmtpClient("smtp.gmail.com", 465)
                    //{
                    //    UseDefaultCredentials = false,
                    //    Credentials = new NetworkCredential("japhetlee.ccf@gmail.com","kazuhira2403"),
                    //    EnableSsl = true,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network
                    //};
                    //client.Send("japhetlee.ccf@gmail.com", model.Email,"Reset Password", $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

                    await _emailService.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                // If user does not exist, don't reveal that to the user for security reasons
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(model);
        }

    }
}
