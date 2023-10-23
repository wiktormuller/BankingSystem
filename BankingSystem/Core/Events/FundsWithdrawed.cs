using BankingSystem.Core.Entities;
using BankingSystem.Shared;

namespace BankingSystem.Core.Events
{
    public record FundsWithdrawed(BankingAccount BankingAccount, Transfer Transfer) : IDomainEvent;
}
