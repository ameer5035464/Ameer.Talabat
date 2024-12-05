using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs
{
    public class FilterParameter
    {
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }
}
