using CodeFirstDatabase.Models.Accounts;
using CodeFirstDatabase.Models.Identity;
using CodeFirstDatabase.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDatabase.Controllers
{
    [AllowAnonymous]
    public class RegisterController(UserManager<ApplicationUser> applicationUser,RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = applicationUser;

        public IActionResult Index()
        {
            if (!_roleManager.RoleExistsAsync(RoleUtils.RoleSuperAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.RoleSuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.RoleAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.RoleAuthor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.RoleUser)).GetAwaiter().GetResult();

            }

            return View();
        }
        [HttpPost]
        public IActionResult Index(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;


                var res = _userManager.CreateAsync(user,model.Password).GetAwaiter().GetResult();
                
                if(res.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, RoleUtils.RoleUser).GetAwaiter().GetResult();

                    ViewBag.SuccessMessage = "User has been registered";
                }
                else
                {
                    List<IdentityError> errorList = res.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));

                    ViewBag.ErrorMessage = errors;
                }

            }
            return View(model);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Cannot create an instance of '{nameof(ApplicationUser)}'");
            }
        }
    }
}
