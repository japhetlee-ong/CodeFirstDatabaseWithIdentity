using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDatabase.Models.Accounts
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{6,}$",ErrorMessage = "Password must contain atleast one uppercase, one numerical and one special character.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage="The password and confirmation password does not match.")]
        public string ConfirmPassword {  get; set; }
    }
}
