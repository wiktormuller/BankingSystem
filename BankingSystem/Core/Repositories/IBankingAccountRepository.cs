using BankingSystem.Core.Entities;

namespace BankingSystem.Core.Repositories
{
    public interface IBankingAccountRepository
    {
        Task<BankingAccount?> GetAsync(Guid id);
        Task<BankingAccount?> GetAsync(string name);
        Task AddAsync(BankingAccount wallet);
        void Update(BankingAccount wallet);
    }
}
