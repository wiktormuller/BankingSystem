namespace BankingSystem.Core.Entities
{
    public class Transfer
    {
        public Guid Id { get; private set; }
        public Guid BankingAccountId { get; private set; }
        public Guid? CorrelationTransferId { get; private set; }
        public decimal Amount { get; private set; }
        public TransferDirection Direction { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transfer(Guid id, Guid bankingAccountId, decimal amount, TransferDirection direction, 
            DateTime createdAt, Guid? correlationTransferId = null)
        {
            Id = id;
            BankingAccountId = bankingAccountId;
            Amount = amount;
            Direction = direction;
            CreatedAt = createdAt;
            CorrelationTransferId = correlationTransferId;

        }

        public static Transfer Incoming(Guid id, Guid bankingAccountId, decimal amount,
            DateTime createdAt, Guid? correlationTransferId = null)
        {
            return new(id, bankingAccountId, amount, TransferDirection.Incoming, createdAt, correlationTransferId);
        }

        public static Transfer Outgoing(Guid id, Guid bankingAccountId, decimal amount, 
            DateTime createdAt, Guid? correlationTransferId = null)
        {
            return new(id, bankingAccountId, amount, TransferDirection.Outgoing, createdAt, correlationTransferId);
        }

        public enum TransferDirection
        {
            Incoming,
            Outgoing
        }
    }
}
