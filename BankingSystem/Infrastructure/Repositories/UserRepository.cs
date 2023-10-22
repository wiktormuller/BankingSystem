using BankingSystem.Core.Entities;
using BankingSystem.Core.Repositories;
using BankingSystem.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Users;
        }

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
        }

        public async Task<User?> GetAsync(Guid userId)
        {
            return await _users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User?> GetAsync(string email)
        {
            return await _users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public Task UpdateAsync(User user)
        {
            _users.Update(user);
            return Task.CompletedTask;
        }
    }
}
