using BankingSystem.Application.Contracts.Responses;
using MediatR;

namespace BankingSystem.Application.Queries
{
    public record GetBankingAccount(Guid BankingAccountId) : IRequest<BankingAccountResponse>;
}
