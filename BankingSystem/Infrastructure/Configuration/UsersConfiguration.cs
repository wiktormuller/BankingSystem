using BankingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingSystem.Infrastructure.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
               .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired();

            builder.HasIndex(u => u.Id);

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
