using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.RolesVms
{
    public class CreateRoleVM
    {
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(20, ErrorMessage = "max lenght is 20 charachter")]
        public string Name { get; set; } = null!;
    }
}