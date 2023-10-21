namespace BankingSystem.Application.Services.Models
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public long Expires { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
