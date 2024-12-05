using Ameer.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs
{
	public class CustomerBasketDTO
	{
		[Required]
		public required string Id { get; set; }
		
		public IEnumerable<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
