namespace BankingSystem.Application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials exception.") { }
    }
}
