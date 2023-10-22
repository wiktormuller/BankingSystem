namespace BankingSystem.Application.Contracts.Requests
{
    public class AddFundsRequest
    {
        public Guid BankingAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
