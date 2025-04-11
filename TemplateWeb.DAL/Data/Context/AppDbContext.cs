using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TemplateWeb.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> LoginHistories { get; set; }

    }
}
