namespace BankingSystem.Application.Contracts.Responses
{
    public record UserMeResponse(Guid Id, string Email, string Role, bool IsActive, DateTime CreatedAt);
}
