using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AuthJWTAspNetWeb.Database
{
    namespace _20240723_SqlDb_Gai.Database
    {
        public class AuthDbContext : IdentityDbContext<IdentityUser>
        {
            public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
