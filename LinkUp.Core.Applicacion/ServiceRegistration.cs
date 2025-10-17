using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LinkUp.Core.Applicacion
{
    public static class ServiceRegistration
    {
        public static void AddApplicacionLayer(this IServiceCollection service) 
        {
            #region Configurations
            service.AddAutoMapper(cfg => { cfg.AddMaps(Assembly.GetExecutingAssembly()); });
            #endregion
        }
    }
}
