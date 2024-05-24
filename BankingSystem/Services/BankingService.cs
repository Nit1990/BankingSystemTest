namespace BankingSystem
{
    
    public class BankingService : IBankingService
    {
        private readonly List<User> _users = new List<User>();

        public async Task<User> CreateUserAsync(string name)
        {
            var user = new User { UserId = Guid.NewGuid().ToString(), Name = name };
            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user != null) _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task<Account> CreateAccountAsync(string userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) throw new ArgumentException("User not found");

            var account = new Account { AccountId = Guid.NewGuid().ToString(), Balance = 100 };
            user.Accounts.Add(account);
            return await Task.FromResult(account);
        }

        public async Task DeleteAccountAsync(string userId, string accountId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) throw new ArgumentException("User not found");

            var account = user.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account != null) user.Accounts.Remove(account);
            await Task.CompletedTask;
        }

        public async Task DepositAsync(string userId, string accountId, decimal amount)
        {
            if (amount > 10000) throw new ArgumentException("Cannot deposit more than 10,000 in a single transaction");

            var account = GetAccount(userId, accountId);
            account.Balance += amount;
            await Task.CompletedTask;
        }

        public async Task WithdrawAsync(string userId, string accountId, decimal amount)
        {
            var account = GetAccount(userId, accountId);
            if (amount > account.Balance * 0.9m) throw new ArgumentException("Cannot withdraw more than 90% of the total balance");
            if (account.Balance - amount < 100) throw new ArgumentException("Account balance cannot be less than 100");

            account.Balance -= amount;
            await Task.CompletedTask;
        }

        private Account GetAccount(string userId, string accountId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) throw new ArgumentException("User not found");

            var account = user.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) throw new ArgumentException("Account not found");

            return account;
        }
    }
}
