using BankingSystem.Application.Contracts.Responses;
using BankingSystem.Application.Queries;
using BankingSystem.Core.Entities;
using BankingSystem.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Queries.Handlers
{
    public class GetUserMeHandler : IRequestHandler<GetUserMe, UserMeResponse>
    {
        private readonly DbSet<User> _users;

        public GetUserMeHandler(UsersDbContext dbContext)
        {
            _users = dbContext.Users;
        }

        public async Task<UserMeResponse?> Handle(GetUserMe request, CancellationToken cancellationToken)
        {
            return await _users
                .Where(user => user.Id == request.UserId)
                .Select(user => new UserMeResponse(user.Id, user.Email, user.Role, user.IsActive, user.CreatedAt))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
