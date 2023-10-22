using BankingSystem.Core.Entities;

namespace BankingSystem.Core.Repositories
{
    public interface IBankingAccountRepository
    {
        Task<BankingAccount?> GetAsync(Guid id);
        Task AddAsync(BankingAccount wallet);
        Task UpdateAsync(BankingAccount wallet);
    }
}
