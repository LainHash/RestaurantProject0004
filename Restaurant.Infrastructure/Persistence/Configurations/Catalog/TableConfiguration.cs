using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Common.Enums;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Persistence.Configurations.Catalog
{
    internal class TableConfiguration : IEntityTypeConfiguration<RestaurantTable>
    {
        public void Configure(EntityTypeBuilder<RestaurantTable> builder)
        {
            builder.ToTable("RestaurantTables");

            builder.HasKey(t => t.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.Property(t => t.TableNumber)
                .IsRequired();

            builder.Property(t => t.FloorNumber)
                .IsRequired();

            builder.Property(t => t.Capacity)
                .IsRequired();

            builder.Property(t => t.Shape)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue(nameof(TableStatus.Available));

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty);

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(t => t.DeletedAt)
                .IsRequired(false);

            // Global query filter – soft delete
            //builder.HasQueryFilter(t => !t.IsDeleted);

            // Unique constraint: one table number per floor
            builder.HasIndex(t => new { t.FloorNumber, t.TableNumber })
                .IsUnique()
                .HasDatabaseName("IX_RestaurantTables_Floor_Table");

            builder.HasIndex(p => p.PublicId)
                .IsUnique()
                .HasDatabaseName("IX_Tables_PublicId");
        }
    }
}
