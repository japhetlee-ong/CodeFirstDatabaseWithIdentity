using System.ComponentModel.DataAnnotations;

namespace CodeFirstDatabase.Models.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
