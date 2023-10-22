using BankingSystem.Shared;

namespace BankingSystem.Core.Exceptions
{
    public class InvalidTransferAmountException : BankingSystemException
    {
        public override string Code { get; } = "invalid_transfer_amount";

        public InvalidTransferAmountException(decimal amount) : base($"Invalid amount of funds for transfer: '{amount}'.") { }
    }
}
