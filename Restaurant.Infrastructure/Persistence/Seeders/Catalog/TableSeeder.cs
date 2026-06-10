using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Catalog
{
    internal class TableSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RestaurantTables.AnyAsync())
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

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.RestaurantTables.Add(new RestaurantTable
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

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RestaurantTables ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RestaurantTables OFF");

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
