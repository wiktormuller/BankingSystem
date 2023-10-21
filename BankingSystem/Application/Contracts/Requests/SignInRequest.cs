namespace BankingSystem.Application.Contracts.Requests
{
    public class SignInRequest
    {
        public string Email { get; set; } // TODO: Add Validation
        public string Password { get; set; }
    }
}
