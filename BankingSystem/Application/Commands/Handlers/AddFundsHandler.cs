using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Handlers
{
    public sealed class AddFundsHandler : IRequestHandler<AddFunds>
    {
        private readonly IBankingAccountRepository _bankingAccountRepository;
        private readonly IClock _clock;

        public AddFundsHandler(IBankingAccountRepository bankingAccountRepository, 
            IClock clock)
        {
            _bankingAccountRepository = bankingAccountRepository;
            _clock = clock;
        }

        public async Task Handle(AddFunds command, CancellationToken cancellationToken)
        {
            var bankingAccount = await _bankingAccountRepository.GetAsync(command.BankingAccountId) 
                ?? throw new BankingAccountNotFound(command.BankingAccountId);

            bankingAccount.AddFunds(Guid.NewGuid(), command.Amount, _clock.CurrentDate());
            await _bankingAccountRepository.UpdateAsync(bankingAccount);
        }
    }
}
