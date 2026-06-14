using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Customers;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Customers
{
    internal class CustomerSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Customers.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "customers.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<CustomerCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Customers.Add(new Customer
                    {
                        Id = record.Id,
                        UserId = record.UserId,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers OFF");

                await transaction.CommitAsync();
            });
        }

        private class CustomerCsvRecord
        {
            public int Id { get; set; }
            public int UserId { get; set; }
        }
    }
}
