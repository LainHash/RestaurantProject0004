using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Persistence.Configurations.Catalog
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.DeletedAt)
                .IsRequired(false);

            // Global query filter – soft delete
            //builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasIndex(c => c.Name)
                .IsUnique()
                .HasDatabaseName("IX_Categories_Name");
        }
    }
}
