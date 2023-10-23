using BankingSystem.Application.Commands;
using BankingSystem.Application.Commands.Handlers;
using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Entities;
using BankingSystem.Core.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BankingSystem.Tests.Unit.Handlers
{
    public class TransferFundsHandlerTests
    {

        private readonly IBankingAccountRepository _bankingAccountRepository;
        private readonly IClock _clock;
        private TransferFundsHandler _sut;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public TransferFundsHandlerTests()
        {
            _bankingAccountRepository = Substitute.For<IBankingAccountRepository>();
            _clock = Substitute.For<IClock>();
        }

        [Fact]
        public async Task Handle_WhenPassingNonExistingBankingAccountId_ShouldThrowsException()
        {
            // Arrange
            var nullBankingAccount = (BankingAccount)null;
            _bankingAccountRepository.GetAsync(Arg.Any<Guid>()).ReturnsNull();

            // Act
            var handleAction = async () => await _sut.Handle(new TransferFunds(Guid.NewGuid(), Guid.NewGuid(), 10.00M), _cts.Token);

            // Assert
            await handleAction.Should()
                .ThrowAsync<BankingAccountNotFoundException>()
                .Where(e => e.Code == "banking_account_not_found");
        }

        [Fact]
        public async Task Handle_WhenPassingCorrectDate_RepositoryShouldBeCalledTwoTimes()
        {
            // Arrange
            BankingAccount? bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingsAccount", DateTime.UtcNow);
            bankingAccount.AddFunds(Guid.NewGuid(), 100.00M, DateTime.UtcNow, Guid.NewGuid());

            _bankingAccountRepository.GetAsync(Arg.Any<Guid>()).Returns(bankingAccount);

            var dateTime = DateTime.Parse("10/23/2023 9:15:12 PM");
            _clock.When(x => x.CurrentDate().Returns(dateTime));

            _sut = new TransferFundsHandler(_bankingAccountRepository, _clock);

            // Act
            await _sut.Handle(new TransferFunds(Guid.NewGuid(), Guid.NewGuid(), 10.00M), _cts.Token);

            // Assert
            _bankingAccountRepository.Received(2);
        }
    }
}
