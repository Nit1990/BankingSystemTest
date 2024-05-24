namespace BankingSystem
{
   public interface IBankingService
    {
        Task<User> CreateUserAsync(string name);
        Task DeleteUserAsync(string userId);
        Task<Account> CreateAccountAsync(string userId);
        Task DeleteAccountAsync(string userId, string accountId);
        Task DepositAsync(string userId, string accountId, decimal amount);
        Task WithdrawAsync(string userId, string accountId, decimal amount);
    }
}
