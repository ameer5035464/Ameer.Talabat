using System.ComponentModel.DataAnnotations;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.Auth
{
	public class LoginDTO
	{
		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		public required string Password { get; set; }
		
		public bool RememberMe { get; set; }
	}
}
