using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Customers;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Infrastructure.Persistence.Configurations.Customers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.Property(p => p.UserId)
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
                .HasDatabaseName("IX_Customers_PublicId");

            builder.HasIndex(p => p.UserId)
                .IsUnique()
                .HasDatabaseName("IX_Customers_UserId");

            // One-to-one: Customer → User
            builder.HasOne(u => u.User)
                .WithOne(c => c.Customer)
                .HasForeignKey<Customer>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
