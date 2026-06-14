using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Common.Enums;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Infrastructure.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.RolerId)
                .IsRequired();

            builder.Property(p => p.PIId)
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
                .HasDatabaseName("IX_Users_PublicId");

            builder.HasIndex(p => p.UserName)
                .HasDatabaseName("IX_Users_UserName");

            builder.HasIndex(p => p.PIId)
                .HasDatabaseName("IX_Users_PerInfo");

            // One-to-one: User → PersonalInformation
            builder.HasOne(u => u.PersonalInformation)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PIId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one: User → Role
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RolerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
