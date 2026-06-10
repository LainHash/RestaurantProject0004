namespace Restaurant.Infrastructure.Persistence.Seed;

public interface IDataSeeder
{
    Task SeedAsync(RestaurantDbContext context);
}
