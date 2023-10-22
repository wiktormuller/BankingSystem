namespace BankingSystem.Application.Exceptions
{
    public class BankingAccountNotFound : Exception
    {
        public BankingAccountNotFound(Guid bankingAccountId) : base($"Banking account with Id: '{bankingAccountId}' was not found.") { }
    }
}
