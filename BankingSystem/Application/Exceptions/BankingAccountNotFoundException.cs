using BankingSystem.Shared;

namespace BankingSystem.Application.Exceptions
{
    public class BankingAccountNotFoundException : BankingSystemException
    {
        public override string Code { get; } = "banking_account_not_found";

        public BankingAccountNotFoundException(Guid bankingAccountId) 
            : base($"Banking account with Id: '{bankingAccountId}' was not found.") { }
    }
}
