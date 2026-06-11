using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Catalog
{
    internal class ProductImageSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductImages.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "product_images.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<ProductImageCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.ProductImages.Add(new ProductImage
                    {
                        Id = record.Id,
                        ProductId = record.ProductId,
                        ImageUrl = record.ImageUrl,
                        IsPrimary = record.IsPrimary,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductImages ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductImages OFF");

                await transaction.CommitAsync();
            });
        }

        private class ProductImageCsvRecord
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
            public bool IsPrimary { get; set; }
        }
    }
}
