using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Inventory
{
    internal class ProductStockSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductStocks.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "product_stocks.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<ProductStockCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.ProductStocks.Add(new ProductStock
                    {
                        Id = record.Id,
                        ProductId = record.ProductId,
                        Price = record.Price,
                        Unit = record.Unit,
                        Quantity = record.Quantity,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductStocks ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductStocks OFF");

                await transaction.CommitAsync();
            });
        }

        private class ProductStockCsvRecord
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public decimal Price { get; set; }
            public string Unit { get; set; } = string.Empty;
            public decimal Quantity { get; set; }
        }
    }
}
