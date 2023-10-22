using BankingSystem.Shared;

namespace BankingSystem.Application.Exceptions
{
    public class InvalidCredentialsException : BankingSystemException
    {
        public override string Code { get; } = "invalid_credentials";

        public InvalidCredentialsException() : base("Invalid credentials exception.") { }
    }
}
