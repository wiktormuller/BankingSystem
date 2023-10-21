namespace BankingSystem.Application.Contracts.Responses
{
    public record JsonWebTokenResponse(string AccessToken, string Email, long Expires, Guid Id, string Role);
}
