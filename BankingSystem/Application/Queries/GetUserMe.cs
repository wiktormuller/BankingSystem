using BankingSystem.Application.Contracts.Responses;
using MediatR;

namespace BankingSystem.Application.Queries
{
    public record GetUserMe(Guid UserId) : IRequest<UserMeResponse>;
}
