using BankingSystem.Core.Events;
using BankingSystem.Core.Exceptions;
using BankingSystem.Shared;

namespace BankingSystem.Core.Entities
{
    public class BankingAccount : AggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IEnumerable<Transfer> Transfers
        {
            get => _transfers;
            set => _transfers = new HashSet<Transfer>(value);
        }

        private HashSet<Transfer> _transfers = new();

        private BankingAccount() { }

        public BankingAccount(Guid id, Guid userId, string name, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Name = name;
            CreatedAt = createdAt;
        }

        public Transfer AddFunds(Guid transferId, decimal amount, DateTime createdAt, Guid? correlationTransferId = null)
        {
            if (amount <= 0)
            {
                throw new InvalidTransferAmountException(amount);
            }

            var transfer = Transfer.Incoming(transferId, Id, amount, createdAt, correlationTransferId);
            _transfers.Add(transfer);
            AddEvent(new FundsAdded(this, transfer));

            return transfer;
        }

        public Transfer WithdrawFunds(Guid transferId, decimal amount, DateTime createdAt, Guid? correlationTransferId = null)
        {
            if (amount <= 0)
            {
                throw new InvalidTransferAmountException(amount);
            }

            if (CurrentAmount() < amount)
            {
                throw new InsufficientWalletFundsException(Id);
            }

            var transfer = Transfer.Outgoing(transferId, Id, amount, createdAt, correlationTransferId);
            _transfers.Add(transfer);
            AddEvent(new FundsWithdrawed(this, transfer));

            return transfer;
        }

        public IEnumerable<Transfer> TransferFunds(BankingAccount receiver, decimal amount, DateTime createdAt)
        {
            var outgoingTransferId = Guid.NewGuid();
            var incomingTransferId = Guid.NewGuid();

            var outgoingTransfer = this.WithdrawFunds(Guid.NewGuid(), amount, createdAt, incomingTransferId);
            var incomingTransfer = receiver.AddFunds(incomingTransferId, amount, createdAt, outgoingTransferId);

            return new[] { outgoingTransfer, incomingTransfer };
        }

        public decimal CurrentAmount()
            => SumIncomingAmount() - SumOutgoingAmount();

        private decimal SumIncomingAmount()
        {
            return _transfers
                .Where(t => t.Direction == Transfer.TransferDirection.Incoming)
                .Sum(t => t.Amount);
        }

        private decimal SumOutgoingAmount()
        {
            return _transfers
                .Where(t => t.Direction == Transfer.TransferDirection.Outgoing)
                .Sum(t => t.Amount);
        }
    }
}
