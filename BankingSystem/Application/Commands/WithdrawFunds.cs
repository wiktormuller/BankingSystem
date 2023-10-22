using MediatR;

namespace BankingSystem.Application.Commands
{
    public record WithdrawFunds(Guid BankingAccountId, decimal Amount) : IRequest;
}
