using BankingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Configuration
{
    public class BankingAccountsConfiguration : IEntityTypeConfiguration<BankingAccount>
    {
        public void Configure(EntityTypeBuilder<BankingAccount> builder)
        {
            builder.ToTable("BankingAccounts");

            builder.HasKey(ba => ba.Id);

            builder.Property(ba => ba.UserId)
                .IsRequired();

            builder.Property(ba => ba.Name)
                .IsRequired();

            builder.Property(ba => ba.CreatedAt)
               .IsRequired();

            builder.Property(ba => ba.Version)
                .IsRequired();

            builder.Property(ba => ba.Version)
                .IsConcurrencyToken(); // Optimistic concurrency to protect the model from concurrent modifications

            builder.HasIndex(ba => ba.Id);

            builder.HasIndex(ba => ba.UserId);

            builder.HasIndex(ba => ba.Name)
                .IsUnique();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(ba => ba.UserId);
        }
    }
}
