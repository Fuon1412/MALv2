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
        public async Task<Account> FindByEmailAsync(string email)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("Account not found");
            return account;
        }
        public bool CheckPassword(Account account, string password)
        {
            return account.Password == password;
        }

        //register service
        public async Task RegisterAsync(Account account, string password)
        {
            account.Password = _passwordHasher.HashPassword(account, password);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

    }
}