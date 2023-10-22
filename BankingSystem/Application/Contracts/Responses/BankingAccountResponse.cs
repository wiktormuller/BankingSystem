namespace BankingSystem.Application.Contracts.Responses
{
    public record BankingAccountResponse(Guid Id, DateTime CreatedAt, decimal Amount, List<TransferResponse> Transfers);
}
