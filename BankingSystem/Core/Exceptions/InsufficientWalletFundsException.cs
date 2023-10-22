using BankingSystem.Shared;

namespace BankingSystem.Core.Exceptions
{
    public class InsufficientWalletFundsException : BankingSystemException
    {
        public override string Code { get; } = "insufficient_wallet_funds";

        public InsufficientWalletFundsException(Guid bankingAccountId) 
            : base($"Insufficient funds for banking account with Id: '{bankingAccountId}'.") { }
    }
}
