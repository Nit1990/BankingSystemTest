namespace BankingSystem
{
    public class User
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
