using Ameer.Talabat.Dashboard.View_Models.RolesVms;
using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.UsersVMs
{
    public class EditUserVM
    {
        [Required]
        public string Id { get; set; } = null!;

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
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        public List<RoleVM>? Roles { get; set; }
    }
}
