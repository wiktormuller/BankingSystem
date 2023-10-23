using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Handlers
{
    public sealed class TransferFundsHandler : IRequestHandler<TransferFunds>
    {
        private readonly IBankingAccountRepository _bankingAccountRepository;
        private readonly IClock _clock;

        public TransferFundsHandler(IBankingAccountRepository bankingAccountRepository, 
            IClock clock)
        {
            _bankingAccountRepository = bankingAccountRepository;
            _clock = clock;
        }


        public async Task Handle(TransferFunds command, CancellationToken cancellationToken)
        {
            var fromBankingAccount = await _bankingAccountRepository.GetAsync(command.FromBankingAccount)
                ?? throw new BankingAccountNotFoundException(command.FromBankingAccount);

            var toBankingAccount = await _bankingAccountRepository.GetAsync(command.ToBankingAccount)
                ?? throw new BankingAccountNotFoundException(command.ToBankingAccount);

            fromBankingAccount.TransferFunds(toBankingAccount, command.Amount, _clock.CurrentDate());

            _bankingAccountRepository.Update(fromBankingAccount);
            _bankingAccountRepository.Update(toBankingAccount);
        }
    }
}
