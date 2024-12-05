using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.AuthVMs
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}
