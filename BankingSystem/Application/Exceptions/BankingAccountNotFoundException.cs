namespace BankingSystem.Application.Exceptions
{
    public class BankingAccountNotFoundException : Exception
    {
        public BankingAccountNotFoundException(Guid bankingAccountId) 
            : base($"Banking account with Id: '{bankingAccountId}' was not found.") { }
    }
}
