using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Core.Domain.Entities.Products;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace Ameer.Talabat.Infrastructure.Persistance.Data
{
    internal class StoreContextIntializer : IStoreContextIntializer
    {
        private readonly StoreContext _storeContext;

        public StoreContextIntializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task IntializeAsync()
        {
            var check = await _storeContext.Database.GetPendingMigrationsAsync();

            if (check.Any())
            {
                await _storeContext.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
        {
            if (!await _storeContext.Brands.AnyAsync())
            {
                var GetBrand = await File.ReadAllTextAsync("../../../Ameer.Talabat.Infrastructure.Persistance/_Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(GetBrand);

                if (brands?.Count > 0)
                {
                    await _storeContext.Brands.AddRangeAsync(brands);
                    await _storeContext.SaveChangesAsync();
                }
            }

            if (!await _storeContext.Categories.AnyAsync())
            {
                var GetCategory = await File.ReadAllTextAsync("../../../Ameer.Talabat.Infrastructure.Persistance/_Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(GetCategory);

                if (categories?.Count > 0)
                {
                    await _storeContext.Categories.AddRangeAsync(categories);
                    await _storeContext.SaveChangesAsync();
                }
            }

            if (!await _storeContext.Products.AnyAsync())
            {
                var GetProduct = await File.ReadAllTextAsync("../../../Ameer.Talabat.Infrastructure.Persistance/_Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(GetProduct);

                if (products?.Count > 0)
                {
                    await _storeContext.Products.AddRangeAsync(products);
                    await _storeContext.SaveChangesAsync();
                }
            }

            if (!await _storeContext.DeliveryMethod.AnyAsync())
            {
                var getFile = await File.ReadAllTextAsync("../../../Ameer.Talabat.Infrastructure.Persistance/_Data/Seeds/delivery.json");
                var DeserializeFile = JsonSerializer.Deserialize<List<DeliveryMethod>>(getFile);

                if (DeserializeFile?.Count > 0)
                {
                    foreach (var item in DeserializeFile)
                    {
                        await _storeContext.Set<DeliveryMethod>().AddAsync(item);
                    }
                    await _storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
