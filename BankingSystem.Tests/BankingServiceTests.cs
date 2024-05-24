using BankingSystem;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankingSystem.Tests
{
    public class BankingServiceTests
    {
        private readonly IBankingService _bankingService;

        public BankingServiceTests()
        {
            _bankingService = new BankingService();
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUser()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            Assert.NotNull(user);
            Assert.Equal("Test User", user.Name);
        }

        [Fact]
        public async Task CreateAccount_ShouldCreateAccountWithInitialBalance()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            var account = await _bankingService.CreateAccountAsync(user.UserId);

            Assert.NotNull(account);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public async Task Deposit_ShouldIncreaseBalance()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            var account = await _bankingService.CreateAccountAsync(user.UserId);

            await _bankingService.DepositAsync(user.UserId, account.AccountId, 500);
            Assert.Equal(600, account.Balance);
        }

        [Fact]
        public async Task Withdraw_ShouldDecreaseBalance()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            var account = await _bankingService.CreateAccountAsync(user.UserId);

            await _bankingService.WithdrawAsync(user.UserId, account.AccountId, 50);
            Assert.Equal(50, account.Balance);
        }

        [Fact]
        public async Task Withdraw_MoreThan90Percent_ShouldThrowException()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            var account = await _bankingService.CreateAccountAsync(user.UserId);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _bankingService.WithdrawAsync(user.UserId, account.AccountId, 91));
            Assert.Equal("Cannot withdraw more than 90% of the total balance", exception.Message);
        }

        [Fact]
        public async Task Deposit_MoreThan10000_ShouldThrowException()
        {
            var user = await _bankingService.CreateUserAsync("Test User");
            var account = await _bankingService.CreateAccountAsync(user.UserId);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _bankingService.DepositAsync(user.UserId, account.AccountId, 10001));
            Assert.Equal("Cannot deposit more than 10,000 in a single transaction", exception.Message);
        }
    }
}
