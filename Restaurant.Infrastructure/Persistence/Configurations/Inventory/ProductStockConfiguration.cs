using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Infrastructure.Persistence.Configurations.Inventory
{
    internal class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.ToTable("ProductStocks");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .UseIdentityColumn();

            builder.Property(ps => ps.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ps => ps.Unit)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ps => ps.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m);

            builder.Property(ps => ps.ProductId)
                .IsRequired();

            // 1-1 unique: each Product has exactly one ProductStock
            builder.HasIndex(ps => ps.ProductId)
                .IsUnique()
                .HasDatabaseName("UX_ProductStocks_ProductId");

            // One-to-one: ProductStock ← Product (FK on ProductStock side)
            builder.HasOne(ps => ps.Product)
                .WithOne(p => p.ProductStock)
                .HasForeignKey<ProductStock>(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
