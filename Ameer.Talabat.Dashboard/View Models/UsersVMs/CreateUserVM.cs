using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.UsersVMs
{
    public class CreateUserVM
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("^01[0125][0-9]{8}$",ErrorMessage ="invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;  
        
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; } = null!;   

        public IEnumerable<string>? Roles { get; set; } = new List<string>();
    }
}
