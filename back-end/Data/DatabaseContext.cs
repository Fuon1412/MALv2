using Models;
using Microsoft.EntityFrameworkCore;
using Models.User;

namespace back_end.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

    public DbSet<Account> Accounts { get; set; }
    }
}