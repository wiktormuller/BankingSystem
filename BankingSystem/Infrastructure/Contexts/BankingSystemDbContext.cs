using BankingSystem.Core.Entities;
using BankingSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Contexts
{
    public class BankingSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BankingAccount> BankingAccounts { get; set; }

        public BankingSystemDbContext(DbContextOptions<BankingSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new BankingAccountsConfiguration());
            modelBuilder.ApplyConfiguration(new TransferConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
