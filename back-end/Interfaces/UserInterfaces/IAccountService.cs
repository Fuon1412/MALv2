using Models.User;
public interface IAccountService
{
    Task<Account?> FindByEmailAsync(string email);
    Task<Account?> FindByUsernameAsync(string username);
    Task RegisterAsync(Account account, string password);
    bool CheckPassword(Account account, string password);
}
