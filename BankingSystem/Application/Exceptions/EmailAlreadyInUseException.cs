using BankingSystem.Shared;

namespace BankingSystem.Application.Exceptions
{
    public class EmailAlreadyInUseException : BankingSystemException
    {
        public override string Code { get; } = "email_already_in_use";

        public EmailAlreadyInUseException() : base("Email already in use.") { }
    }
}
