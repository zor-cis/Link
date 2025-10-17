using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Infrastructure.Identity.Contexts;
using LinkUp.Infrastructure.Identity.Seeds;
using LinkUp.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkUp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityLayerForWebApp(this IServiceCollection service, IConfiguration confi) 
        {
            service.GeneralConfiguration(confi);

            #region Identity

            service.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 8; 
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true; 

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
                opt.Lockout.MaxFailedAccessAttempts = 5; 

                opt.User.RequireUniqueEmail = true; 
                opt.SignIn.RequireConfirmedEmail = true; 
            });

            service.AddIdentityCore<AppUser>()
                .AddSignInManager()
                .AddEntityFrameworkStores<IdentityLinkUpContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

            service.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(12);
            });

            service.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(180);
                opt.LoginPath = "/Login";
                opt.AccessDeniedPath = "/Login/AccessDenied";  
            });

            #endregion

            #region Service
            service.AddScoped<IAccountServiceForWebApp, AccountServiceForWebApp>();
            #endregion
        }

        public static async Task RunIdentitySeedAsync(this IServiceProvider service) 
        {
            using var scope = service.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            await DefaultUserApp.SendAsync(userManager);
        }

        #region Private Methods

        private static void GeneralConfiguration(this IServiceCollection service, IConfiguration confi) 
        {
            #region Context 

            if (confi.GetValue<bool>("InMemoryDatabase"))
            {
                service.AddDbContext<IdentityLinkUpContext>(op => op.UseInMemoryDatabase("LinkUp"));
            }
            else
            {
                var connectionString = confi.GetConnectionString("DefaultConnSql");
                service.AddDbContext<IdentityLinkUpContext>(
                    
                (ServiceProvider, op) => 
                { 
                    op.EnableSensitiveDataLogging();
                    op.UseSqlServer(connectionString, m => m.MigrationsAssembly(typeof(IdentityLinkUpContext).Assembly.FullName)); 
                },

                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped
                
                );
            }

            #endregion
        }

        #endregion
    }
}
