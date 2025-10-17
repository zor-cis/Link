using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkUp.Infrastructure.Persistence.Contexts
{
    public class LinkUpContext : DbContext
    {
        public LinkUpContext(DbContextOptions<LinkUpContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
