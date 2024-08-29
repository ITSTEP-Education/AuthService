using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthJWTAspNetWeb.Models;

namespace AuthJWTAspNetWeb.Database
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Car> Cars { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarConfuguration());
        }
    }
}
