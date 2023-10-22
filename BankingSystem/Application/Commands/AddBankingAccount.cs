using MediatR;

namespace BankingSystem.Application.Commands
{
    public record AddBankingAccount(string Name, Guid UserId) : IRequest<Guid>;
}
