using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Core.Domain.Entities.Products;
using System.Reflection;

namespace Ameer.Talabat.Infrastructure.Persistance.Data
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type =>
                type.Namespace!.Contains("Ameer.Talabat.Infrastructure.Persistance.Data.Configurations")
            );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            base.OnConfiguring(optionsBuilder);


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }

    }
}
