namespace BankingSystem.Core.Exceptions
{
    public class InsufficientWalletFundsException : Exception
    {
        public InsufficientWalletFundsException(Guid bankingAccountId) 
            : base($"Insufficient funds for banking account with Id: '{bankingAccountId}'.") { }
    }
}
