using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Identity
{
    internal class RoleSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "roles.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<RoleCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Roles.Add(new Role
                    {
                        Id = record.Id,
                        Name = record.Name,
                        Level = record.Level,
                        Description = record.Description ?? string.Empty,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF");

                await transaction.CommitAsync();
            });
        }

        private class RoleCsvRecord
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Level { get; set; }
            public string? Description { get; set; }
        }
    }
}
