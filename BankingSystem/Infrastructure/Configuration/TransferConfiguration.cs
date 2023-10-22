using BankingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static BankingSystem.Core.Entities.Transfer;

namespace BankingSystem.Infrastructure.Configuration
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CorrelationTransferId);

            builder.Property(x => x.BankingAccountId)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Direction)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (TransferDirection)Enum.Parse(typeof(TransferDirection), v));
        }
    }
}
