using MediatR;

namespace BankingSystem.Application.Commands
{
    public record TransferFunds(Guid FromBankingAccount, Guid ToBankingAccount, decimal Amount) : IRequest;
}
