using Models.User;
public interface IAccountService
{
    Task<Account> FindByEmailAsync(string email);
    Task RegisterAsync(Account account, string password);
    bool CheckPassword(Account account, string password);
}
