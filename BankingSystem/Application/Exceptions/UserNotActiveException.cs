using BankingSystem.Shared;

namespace BankingSystem.Application.Exceptions
{
    public class UserNotActiveException : BankingSystemException
    {
        public override string Code { get; } = "user_not_active";

        public UserNotActiveException(string email) : base($"User with email: '{email}' is not active") { }
    }
}
