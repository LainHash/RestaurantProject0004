using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Persistence.Repositories.Catalog;
using Restaurant.Infrastructure.Persistence.Seeders;
using Restaurant.Infrastructure.Persistence.Seeders.Catalog;
using Restaurant.Infrastructure.Persistence.Seeders.Inventory;

namespace Restaurant.Infrastructure.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── Database ─────────────────────────────────────────────────────
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MyConnectString"),
                    sqlOptions => sqlOptions.MigrationsAssembly(
                        typeof(RestaurantDbContext).Assembly.FullName)));

            // ── Seeders ──────────────────────────────────────────────────────
            services.AddScoped<CategorySeeder>();
            services.AddScoped<TableSeeder>();
            services.AddScoped<ProductSeeder>();
            services.AddScoped<ProductStockSeeder>();
            services.AddScoped<ProductImageSeeder>();
            services.AddScoped<DatabaseSeeder>();

            // ── Repositories ─────────────────────────────────────────────────
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        /// <summary>
        /// Applies pending EF Core migrations and runs database seeders.
        /// Call this from Program.cs after the app is built.
        /// </summary>
        public static async Task InitialiseDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;

            var context = sp.GetRequiredService<RestaurantDbContext>();
            await context.Database.MigrateAsync();

            var seeder = sp.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedAllAsync();
        }
    }
}
