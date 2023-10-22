namespace BankingSystem.Infrastructure.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public TimeSpan Expiry { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public string Challenge { get; set; } = "Bearer";
    }
}
