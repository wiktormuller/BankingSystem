namespace BankingSystem.Application.Contracts.Requests
{
    public class TransferFundsRequest
    {
        public Guid FromBankingAccountId { get; set; }
        public Guid ToBankingAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
