using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.BrandsVMs
{
    public class BrandCreateVm
    {
        [Required]
        public string Name { get; set; }
    }
}
