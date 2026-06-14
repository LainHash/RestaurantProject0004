using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Persistence.Repositories.Catalog;
using Restaurant.Infrastructure.Persistence.Repositories.Identity;
using Restaurant.Infrastructure.Persistence.Seeders;
using Restaurant.Infrastructure.Persistence.Seeders.Catalog;
using Restaurant.Infrastructure.Persistence.Seeders.Customers;
using Restaurant.Infrastructure.Persistence.Seeders.Identity;
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
            // Identity seeders (must be registered first due to FK dependencies)
            services.AddScoped<RoleSeeder>();
            services.AddScoped<PersonalInfoSeeder>();
            services.AddScoped<UserSeeder>();
            services.AddScoped<CustomerSeeder>();

            // Catalog seeders
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
            services.AddScoped<IUserRepository, UserRepository>();

            // ── Authentication Services ──────────────────────────────────────
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

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
