
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;
using LinkUp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkUp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection service, IConfiguration confi) 
        {
            #region Contexts

            if (confi.GetValue<bool>("InMemoryDatabase"))
            {
                service.AddDbContext<LinkUpContext>(op => op.UseInMemoryDatabase("LinkUp"));
            }
            else
            {
                var connectionString = confi.GetConnectionString("DefaultConnSql");
                service.AddDbContext<LinkUpContext>(

                (ServiceProvider, op) =>
                {
                    op.EnableSensitiveDataLogging();
                    op.UseSqlServer(connectionString, m => m.MigrationsAssembly(typeof(LinkUpContext).Assembly.FullName));
                },

                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped

                );
            }
            #endregion
            
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
