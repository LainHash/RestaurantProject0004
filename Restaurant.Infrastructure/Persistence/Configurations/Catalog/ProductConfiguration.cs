using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Persistence.Configurations.Catalog
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .HasDefaultValue(string.Empty);

            builder.Property(p => p.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.DeletedAt)
                .IsRequired(false);

            // Global query filter – soft delete
            //builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasIndex(p => p.PublicId)
                .IsUnique()
                .HasDatabaseName("IX_Products_PublicId");

            builder.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");

            // Many-to-one: Product → Category
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
