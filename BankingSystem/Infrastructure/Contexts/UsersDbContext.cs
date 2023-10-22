using BankingSystem.Core.Entities;
using BankingSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Contexts
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
