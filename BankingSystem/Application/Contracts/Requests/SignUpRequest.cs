namespace BankingSystem.Application.Contracts.Requests
{
    public class SignUpRequest
    {
        public string Email { get; set; } // TODO: Add Validation
        public string Password { get; set; }
    }
}
