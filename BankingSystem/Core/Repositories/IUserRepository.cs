using BankingSystem.Core.Entities;

namespace BankingSystem.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(Guid userId);
        Task<User?> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
