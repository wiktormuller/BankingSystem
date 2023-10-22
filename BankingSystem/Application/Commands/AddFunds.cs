using MediatR;

namespace BankingSystem.Application.Commands
{
    public record AddFunds(Guid BankingAccountId, decimal Amount) : IRequest;
}
