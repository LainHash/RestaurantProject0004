using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Persistence;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace Restaurant.Infrastructure.Persistence.Seeders.Catalog
{
    internal class CategorySeeder
    {
        private readonly RestaurantDbContext _context;

        public CategorySeeder(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Categories.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "categories.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);

            var categories = csv.GetRecords<Category>().ToList();

            await _context.Categories.AddRangeAsync(categories);

            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories OFF");
        }
    }
}
