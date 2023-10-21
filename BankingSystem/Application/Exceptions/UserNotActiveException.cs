namespace BankingSystem.Application.Exceptions
{
    public class UserNotActiveException : Exception
    {
        public UserNotActiveException(string email) : base($"User with email: '{email}' is not active") { }
    }
}
