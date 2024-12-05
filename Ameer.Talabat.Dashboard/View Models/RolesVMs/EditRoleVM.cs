using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.RolesVms
{
    public class EditRoleVM
    {
        [Required(ErrorMessage = "Role Name Is Required")]
        public string Name { get; set; } = null!;

        [Required]
        public string id { get; set; } = null!;

    }
}
