using back_end.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using back_end.Models.User;
using back_end.Interfaces.UserInterfaces;

namespace back_end.Services.UserServices
{
    public class AccountServices(DatabaseContext context, IPasswordHasher<Account> passwordHasher) : IAccountService
    {
        private readonly DatabaseContext _context = context;
        private readonly IPasswordHasher<Account> _passwordHasher = passwordHasher;

        //register service
        public async Task RegisterAsync(Account account, string password)
        {
            account.PasswordHash = _passwordHasher.HashPassword(account, password);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> FindByUsernameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Username == username);
        }
        //login service
        public async Task<Account?> FindByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public bool CheckPassword(Account account, string password)
        {
            if (string.IsNullOrEmpty(account.PasswordHash))
            {
                throw new ArgumentNullException(nameof(account.PasswordHash), "Password hash cannot be null or empty.");
            }
            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
        //change password service
        public async Task ChangePasswordAsync(Account account, string newPassword)
        {
             account.PasswordHash = _passwordHasher.HashPassword(account, newPassword);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }
    }
}