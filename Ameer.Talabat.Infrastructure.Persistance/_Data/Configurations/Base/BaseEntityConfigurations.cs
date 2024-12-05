using Ameer.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base
{
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
                    where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(B => B.Id)
                .ValueGeneratedOnAdd();

            builder.Property(B => B.CreatedBy).ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(B => B.CreatedOn)
                .IsRequired();

            builder.Property(B => B.LastModifiedBy)
                .IsRequired();

            builder.Property(B => B.LastModifiedOn)
                .IsRequired();
        }
    }
}
