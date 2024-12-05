using Ameer.Talabat.Core.Application.Abstraction.Pagination;
using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; }

        public string? PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; }
        public virtual string? Brand { get; set; }

        public int? CategoryId { get; set; }
        public virtual string? Category { get; set; }
            
        public string CreatedBy { get; set; }
    }
}
