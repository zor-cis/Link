using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.Services;
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

            #region Services

            service.AddScoped<IPublicationService, PublicationService>();
            service.AddScoped<IPostCommenService, PostCommenService>();
            service.AddScoped<IReactionService, ReactionService>();
            service.AddScoped<IReplyService, ReplyService>();
            service.AddScoped<IFriendshipRequestService, FriendshipRequestService>();
            service.AddScoped<IFriendService, FriendService>();
            service.AddScoped<ISettingGameBattleShipService, SettingGameBattleShipService>();
            service.AddScoped<IBattleshipGameService, BattleshipGameService>();

            #endregion
        }
    }
}
