namespace BankingSystem.Application.Contracts.Responses
{
    public record TransferResponse(Guid Id, string Operation, Guid BankingAccountId, decimal Amount, DateTime CreatedAt);
}
