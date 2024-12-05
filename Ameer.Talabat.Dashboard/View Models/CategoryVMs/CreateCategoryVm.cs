using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.CategoryVMs
{
    public class CreateCategoryVm
    {
        [Required]
        public string Name { get; set; }
    }
}
