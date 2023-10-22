using BankingSystem.Core.Entities;
using BankingSystem.Core.Repositories;
using BankingSystem.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories
{
    public class BankingAccountRepository : IBankingAccountRepository
    {
        private readonly BankingSystemDbContext _context;
        private readonly DbSet<BankingAccount> _bankingAccounts;

        public BankingAccountRepository(BankingSystemDbContext context)
        {
            _context = context;
            _bankingAccounts = _context.BankingAccounts;
        }

        public Task<BankingAccount?> GetAsync(Guid id)
            => _bankingAccounts
                .Include(x => x.Transfers)
                .SingleOrDefaultAsync(x => x.Id == id);

        public Task<BankingAccount?> GetAsync(string name)
            => _bankingAccounts
                .Include(x => x.Transfers)
                .SingleOrDefaultAsync(x => x.Name == name);

        public async Task AddAsync(BankingAccount bankingAccount)
        {
            await _bankingAccounts.AddAsync(bankingAccount);
        }

        public async Task UpdateAsync(BankingAccount bankingAccount)
        {
            _bankingAccounts.Update(bankingAccount);
        }
    }
}
