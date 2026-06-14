using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Persistence.Seed;
using System.Globalization;

namespace Restaurant.Infrastructure.Persistence.Seeders.Identity
{
    internal class PersonalInfoSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.PersonalInformations.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Persistence", "Data", "personal_infos.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<PersonalInfoCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.PersonalInformations.Add(new PersonalInformation
                    {
                        Id = record.Id,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        DOB = record.DOB,
                        Gender = record.Gender,
                        Country = record.Country,
                        City = record.City,
                        Address = record.Address,
                        Phone = record.Phone,
                        CitizenCardId = record.CitizenCardId,
                    });
                }

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PersonalInformations ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PersonalInformations OFF");

                await transaction.CommitAsync();
            });
        }

        private class PersonalInfoCsvRecord
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateOnly DOB { get; set; }
            public bool Gender { get; set; }
            public string Country { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string CitizenCardId { get; set; } = string.Empty;
        }
    }
}
