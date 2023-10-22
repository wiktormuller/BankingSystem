namespace BankingSystem.Application.Contracts.Requests
{
    public class WithdrawFundsRequest
    {
        public Guid BankingAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
