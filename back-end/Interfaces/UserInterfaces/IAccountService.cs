using Models.User;
public interface IAccountService
{
    Task<Account> FindByEmailAsync(string email);
    bool CheckPassword(Account account, string password);
}
