using BankingSystem.Core.Entities;
using BankingSystem.Shared;

namespace BankingSystem.Core.Events
{
    public record FundsAdded(BankingAccount BankingAccount, Transfer Transfer) : IDomainEvent;
}
