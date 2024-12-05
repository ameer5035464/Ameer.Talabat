namespace Ameer.Talabat.Core.Domain.Common
{
	public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
        public TKey Id { get; set; }

        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string? LastModifiedBy { get; set; } = string.Empty ;

        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
	}
}   
