namespace BankingSystem.Core.Exceptions
{
    public class InvalidTransferAmountException : Exception
    {
        public InvalidTransferAmountException(decimal amount) : base($"Invalid amount of funds for transfer: '{amount}'.") { }
    }
}
