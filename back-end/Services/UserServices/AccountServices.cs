using back_end.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models.User;

namespace Services.UserServices
{
    public class AccountServices : IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;
        public AccountServices(DatabaseContext context, IPasswordHasher<Account> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        //login service
        public async Task<Account?> FindByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public bool CheckPassword(Account account, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }

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
    }
}