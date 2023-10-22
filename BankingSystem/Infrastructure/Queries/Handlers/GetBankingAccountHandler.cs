using BankingSystem.Application.Contracts.Responses;
using BankingSystem.Application.Queries;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Infrastructure.Queries.Handlers
{
    public class GetBankingAccountHandler : IRequestHandler<GetBankingAccount, BankingAccountResponse>
    {
        private readonly IBankingAccountRepository _bankingAccountRepository;

        public GetBankingAccountHandler(IBankingAccountRepository bankingAccountRepository)
        {
            _bankingAccountRepository = bankingAccountRepository;
        }

        public async Task<BankingAccountResponse?> Handle(GetBankingAccount request, CancellationToken cancellationToken)
        {
            var bankingAccount = await _bankingAccountRepository.GetAsync(request.BankingAccountId);

            if (bankingAccount is not null)
            {
                var transfers = bankingAccount.Transfers.Select(x
                    => new TransferResponse(x.Id, x.Direction.ToString().ToLowerInvariant(), x.BankingAccountId, x.Amount, x.CreatedAt))
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

                return new BankingAccountResponse(bankingAccount.Id, bankingAccount.CreatedAt, bankingAccount.CurrentAmount(), transfers);
            }

            return null;
        }
    }
}
