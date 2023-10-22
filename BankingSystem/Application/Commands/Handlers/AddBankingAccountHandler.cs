using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Entities;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Handlers
{
    public sealed class AddBankingAccountHandler : IRequestHandler<AddBankingAccount, Guid>
    {
        private readonly IBankingAccountRepository _bankingAccountRepository;
        private readonly IClock _clock;

        public AddBankingAccountHandler(IBankingAccountRepository bankingAccountRepository, 
            IClock clock)
        {
            _bankingAccountRepository = bankingAccountRepository;
            _clock = clock;
        }

        public async Task<Guid> Handle(AddBankingAccount command, CancellationToken cancellationToken)
        {
            var existingBankingAccount = await _bankingAccountRepository.GetAsync(command.Name);

            if (existingBankingAccount is not null)
            {
                throw new BankingAccountAlreadyExistsException(command.Name);
            }

            var bankingAccount = 
                new BankingAccount(Guid.NewGuid(), command.UserId, command.Name, _clock.CurrentDate());

            await _bankingAccountRepository.AddAsync(bankingAccount);

            return bankingAccount.Id;
        }
    }
}
