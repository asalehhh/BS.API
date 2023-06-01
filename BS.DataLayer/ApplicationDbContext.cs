using BS.DataLayer.Models.Accounts;
using BS.DataLayer.Models.Transactions;
using BS.DataLayer.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BS.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
    }
}
