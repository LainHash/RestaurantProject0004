using Restaurant.Infrastructure.Persistence.Seeders.Catalog;

namespace Restaurant.Infrastructure.Persistence.Seeders
{
    internal class DatabaseSeeder
    {
        private readonly CategorySeeder _categorySeeder;
        private readonly TableSeeder _tableSeeder;

        public DatabaseSeeder(
            CategorySeeder categorySeeder,
            TableSeeder tableSeeder)
        {
            _categorySeeder = categorySeeder;
            _tableSeeder = tableSeeder;
        }

        public async Task SeedAllAsync()
        {
            await _categorySeeder.SeedAsync();
            await _tableSeeder.SeedAsync();
        }
    }
}
