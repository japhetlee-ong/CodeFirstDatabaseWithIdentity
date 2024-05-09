using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDatabase.Models.Accounts
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get;set; }
        [Required(ErrorMessage = "Password is requred")]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        [DisplayName("Remember me :)")]
        public bool RememberMe { get; set; }
    }
}
