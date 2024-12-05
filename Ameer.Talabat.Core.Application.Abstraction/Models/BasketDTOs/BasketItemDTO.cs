using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs
{
	public class BasketItemDTO
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public required string ProductName { get; set; }
		[Required]
		public string? PictureUrl { get; set; }
		[Required]
		[Range(.1,double.MaxValue,ErrorMessage ="Price is invalid!")]
		public decimal Price { get; set; }
		[Required]
		[Range(1,int.MaxValue,ErrorMessage ="Quantity is invalid")]
		public int Quantity { get; set; }
		[Required]
		public string? Brand { get; set; }
		[Required]
		public string? Category { get; set; }
	}
}
