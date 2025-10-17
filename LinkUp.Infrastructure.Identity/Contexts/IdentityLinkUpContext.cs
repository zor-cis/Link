using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Infrastructure.Identity.Contexts
{
    public class IdentityLinkUpContext : IdentityDbContext<AppUser>
    {
        public IdentityLinkUpContext(DbContextOptions<IdentityLinkUpContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
        }
    }
}
