using BankingSystem.Shared;

namespace BankingSystem.Application.Exceptions
{
    public class BankingAccountAlreadyExistsException : BankingSystemException
    {
        public override string Code { get; } = "banking_account_already_exists";

        public BankingAccountAlreadyExistsException(string name) : base($"Banking account with name: '{name}' already exists.")
        {
        }
    }
}
