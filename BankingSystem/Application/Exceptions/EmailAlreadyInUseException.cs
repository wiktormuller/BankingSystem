namespace BankingSystem.Application.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException() : base("Email already in use.") { }
    }
}
