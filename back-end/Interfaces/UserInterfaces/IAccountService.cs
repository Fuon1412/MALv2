using back_end.Models.User;
namespace back_end.Interfaces.UserInterfaces
{
    public interface IAccountService
    {
        Task<Account?> FindByEmailAsync(string email);
        Task<Account?> FindByUsernameAsync(string username);
        Task RegisterAsync(Account account, string password);
        Task ChangePasswordAsync(Account account, string newPassword);
        bool CheckPassword(Account account, string password);
    }

}