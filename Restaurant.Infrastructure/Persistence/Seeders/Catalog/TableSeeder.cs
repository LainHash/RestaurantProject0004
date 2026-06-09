using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Persistence;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Catalog
{
    internal class TableSeeder
    {
        private readonly RestaurantDbContext _context;

        public TableSeeder(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.RestaurantTables.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "restaurant_tables.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);

            var tables = csv.GetRecords<RestaurantTable>().ToList();

            await _context.RestaurantTables.AddRangeAsync(tables);
            await _context.SaveChangesAsync();
        }
    }
}
