using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance._Identity.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.FirstName)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder.Property(a => a.LastName)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder.HasOne(a => a.Address)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Address>(a => a.USerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
