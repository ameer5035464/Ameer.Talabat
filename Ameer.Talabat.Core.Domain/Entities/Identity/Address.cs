using System.ComponentModel.DataAnnotations.Schema;

namespace Ameer.Talabat.Core.Domain.Entities.Identity
{
	public class Address
	{

        public Address()
        {
            Id = Guid.NewGuid().ToString();
        }

        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        public required string USerId { get; set; }
        public virtual required ApplicationUser ApplicationUser { get; set; }
    }
}