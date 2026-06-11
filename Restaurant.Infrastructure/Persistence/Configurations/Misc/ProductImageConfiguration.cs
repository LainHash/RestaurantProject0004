using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Infrastructure.Persistence.Configurations.Catalog
{
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Id)
                .UseIdentityColumn();

            builder.Property(pi => pi.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.Property(pi => pi.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pi => pi.IsPrimary)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(pi => pi.ProductId)
                .IsRequired();

            builder.Property(pi => pi.CreatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.Property(pi => pi.UpdatedAt)
                .HasDefaultValueSql("sysdatetime()")
                .IsRequired();

            builder.HasIndex(pi => pi.PublicId)
                .IsUnique()
                .HasDatabaseName("IX_ProductImages_PublicId");

            builder.HasIndex(pi => pi.ProductId)
                .IsUnique()
                .HasFilter("[IsPrimary] = 1")
                .HasDatabaseName("IX_ProductImages_ProductId_IsPrimary");

            // Many-to-one: ProductImage → Product
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
