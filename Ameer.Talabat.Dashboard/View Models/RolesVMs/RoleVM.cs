using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.RolesVms
{
    public class RoleVM
    {

        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Role Name Is Required")]
        public string Name { get; set; } = null!;

        public bool IsSelected { get; set; }
    }
}
