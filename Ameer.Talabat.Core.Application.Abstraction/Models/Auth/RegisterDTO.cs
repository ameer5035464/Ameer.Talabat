using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.Auth
{
	public class RegisterDTO
	{
		[Required]
        public required string FirstName { get; set; }

		[Required]
		public required string LastName { get; set; }

		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		[RegularExpression("^01[0125][0-9]{8}$",ErrorMessage ="invalid Phone Number")]
        public required string PhoneNumber { get; set; }
		[Required]
        public required string Password { get; set; }
    }
}
