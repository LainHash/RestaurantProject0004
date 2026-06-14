using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;
using BCrypt.Net;

namespace Restaurant.Infrastructure.Persistence.Seeders.Identity
{
    internal class UserSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "users.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<UserCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    // Hash password using bcrypt with cost factor 10
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(record.PasswordHash, workFactor: 10);

                    context.Users.Add(new User
                    {
                        Id = record.Id,
                        UserName = record.UserName,
                        Email = record.Email,
                        PasswordHash = passwordHash,
                        Status = record.Status,
                        PIId = record.PIId,
                        RolerId = record.RolerId,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF");

                await transaction.CommitAsync();
            });
        }

        private class UserCsvRecord
        {
            public int Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public int PIId { get; set; }
            public int RolerId { get; set; }
        }
    }
}
