using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Persistence.Seed;
using Restaurant.Infrastructure.Persistence.Seeders.Catalog;
using Restaurant.Infrastructure.Persistence.Seeders.Inventory;

namespace Restaurant.Infrastructure.Persistence.Seeders
{
    internal class DatabaseSeeder
    {
        private readonly RestaurantDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeeder(RestaurantDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAllAsync()
        {
            // Order matters: Categories first (FK dependency)
            await SeedAsync<CategorySeeder>(_context);
            await SeedAsync<TableSeeder>(_context);

            // Products depend on Categories; ProductStocks depend on Products
            await SeedAsync<ProductSeeder>(_context);
            await SeedAsync<ProductStockSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
