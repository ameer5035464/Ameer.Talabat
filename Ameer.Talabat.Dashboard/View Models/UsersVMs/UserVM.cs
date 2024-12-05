using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.UsersVMs
{
    public class UserVM
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public IEnumerable<string>? Roles { get; set; }

    }
}
