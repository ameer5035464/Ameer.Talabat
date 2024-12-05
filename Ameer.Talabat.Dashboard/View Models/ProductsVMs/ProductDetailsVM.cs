using Ameer.Talabat.Core.Domain.Entities.Products;

namespace Ameer.Talabat.Dashboard.View_Models.ProductsVMs
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? PictureUrl { get; set; }

        public decimal Price { get; set; }

        public  string Brand { get; set; }

        public string Category { get; set; }
    }
}
