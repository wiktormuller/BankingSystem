using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Repositories;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            var (fromWalletId, toWalletId, amount) = command;

            var fromBankingAccount = await _bankingAccountRepository.GetAsync(command.FromBankingAccount)
                ?? throw new BankingAccountNotFoundException(command.FromBankingAccount);

            var toBankingAccount = await _bankingAccountRepository.GetAsync(toWalletId)
                ?? throw new BankingAccountNotFoundException(command.ToBankingAccount);

            fromBankingAccount.TransferFunds(toBankingAccount, amount, _clock.CurrentDate());

            await _bankingAccountRepository.UpdateAsync(fromBankingAccount);
            await _bankingAccountRepository.UpdateAsync(toBankingAccount);
        }
    }
}
