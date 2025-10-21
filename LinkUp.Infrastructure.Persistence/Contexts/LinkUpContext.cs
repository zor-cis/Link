using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkUp.Infrastructure.Persistence.Contexts
{
    public class LinkUpContext : DbContext
    {
        public LinkUpContext(DbContextOptions<LinkUpContext> options) : base(options) { }

        public DbSet<Publication> Publications { get; set; }
        public DbSet<PostCommen> PostCommens { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
