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

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<TableCsvRecord>().ToList();

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    _context.RestaurantTables.Add(new RestaurantTable
                    {
                        Id = record.Id,
                        TableNumber = record.TableNumber,
                        FloorNumber = record.FloorNumber,
                        Shape = record.Shape,
                        Capacity = record.Capacity,
                        Status = record.Status,
                        Description = record.Description ?? string.Empty,
                    });
                }

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RestaurantTables ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RestaurantTables OFF");

                await transaction.CommitAsync();
            });
        }

        private class TableCsvRecord
        {
            public int Id { get; set; }
            public int TableNumber { get; set; }
            public int FloorNumber { get; set; }
            public string Shape { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public string Status { get; set; } = string.Empty;
            public string? Description { get; set; }
        }
    }
}
