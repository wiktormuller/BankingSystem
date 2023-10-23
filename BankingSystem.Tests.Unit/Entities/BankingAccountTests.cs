using BankingSystem.Core.Entities;
using BankingSystem.Core.Exceptions;
using FluentAssertions;

namespace BankingSystem.Tests.Unit.Entities
{
    public class BankingAccountTests
    {
        [Fact]
        public void WhenAddFunds_PassingAmountEqualsZero_ShouldThrowsException()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var zeroAmount = 0.00M;

            // Act
            Func<Transfer> transferFunc =
                () => bankingAccount.AddFunds(Guid.NewGuid(), zeroAmount, DateTime.UtcNow);

            // Assert
            var exception = transferFunc.Should()
                .Throw<InvalidTransferAmountException>()
                .Where(e => e.Code == "invalid_transfer_amount");
        }

        [Fact]
        public void WhenAddFunds_PassingAmountLessThanZero_ShouldThrowsException()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var zeroAmount = -1.00M;

            // Act
            Func<Transfer> transferFunc =
                () => bankingAccount.AddFunds(Guid.NewGuid(), zeroAmount, DateTime.UtcNow);

            // Assert
            var exception = transferFunc.Should()
                .Throw<InvalidTransferAmountException>()
                .Where(e => e.Code == "invalid_transfer_amount");
        }

        [Fact]
        public void WhenAddFunds_PassingCorrectData_ShouldReturnsTransfer()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var amount = 50.00M;

            // Act
            var transfer = bankingAccount.AddFunds(Guid.NewGuid(), amount, DateTime.UtcNow);

            // Assert
            transfer.Amount.Should().Be(amount);
        }

        [Fact]
        public void WhenAddFunds_PassingCorrectData_ShouldEmitDomainEvent()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var amount = 50.00M;

            // Act
            var transfer = bankingAccount.AddFunds(Guid.NewGuid(), amount, DateTime.UtcNow);

            // Assert
            bankingAccount.Events.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact]
        public void WhenWithdrawFunds_PassingAmountEqualsZero_ShouldThrowsException()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var zeroAmount = 0.00M;

            // Act
            Func<Transfer> transferFunc =
                () => bankingAccount.WithdrawFunds(Guid.NewGuid(), zeroAmount, DateTime.UtcNow);

            // Assert
            var exception = transferFunc.Should()
                .Throw<InvalidTransferAmountException>()
                .Where(e => e.Code == "invalid_transfer_amount");
        }

        [Fact]
        public void WhenWithdrawFunds_PassingAmountLessThanZero_ShouldThrowsException()
        {
            // Arrange
            var bankingAccount = new BankingAccount(Guid.NewGuid(), Guid.NewGuid(), "SavingAccount", DateTime.UtcNow);
            var zeroAmount = -1.00M;

            // Act
            Func<Transfer> transferFunc =
                () => bankingAccount.WithdrawFunds(Guid.NewGuid(), zeroAmount, DateTime.UtcNow);

            // Assert
            var exception = transferFunc.Should()
                .Throw<InvalidTransferAmountException>()
                .Where(e => e.Code == "invalid_transfer_amount");
        }
    }
}
