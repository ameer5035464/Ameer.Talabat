using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Dashboard.View_Models.ProductsVMs
{
    public class CreateEditProductVM
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

        public string? PictureUrl { get; set; }
        public IFormFile? Image { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }
    }
}
