using LinkUp.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkUp.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedLayer(this IServiceCollection service, IConfiguration confi) 
        {
            #region Context

            service.Configure<EmailService>(confi.GetSection("EmailSetting"));

            #endregion

            #region Service 

            service.AddScoped<IEmailService, EmailService>();

            #endregion
        }
    }
}
