using BankingSystem.Infrastructure.Contexts;

namespace BankingSystem.Infrastructure.UnitOfWork
{
    public class BankingSystemUnitOfWork : SqlServerUnitOfWork<BankingSystemDbContext>
    {
        public BankingSystemUnitOfWork(BankingSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
