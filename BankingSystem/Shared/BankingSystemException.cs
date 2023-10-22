namespace BankingSystem.Shared
{
    public class BankingSystemException : Exception
    {
        public virtual string Code { get; }

        public BankingSystemException(string message) : base(message) { }
    }
}
