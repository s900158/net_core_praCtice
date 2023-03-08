using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using net_core_practice.Services.Home;
using net_core_practice.Services.Home.DB;
using net_core_practice.Services.Redis;
using net_core_practice.Services.Session.Wappers;

namespace net_core_practice.Utilis.Extensions
{
    public static class CustomServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            #region Session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISessionWapper, SessionWapper>();
            #endregion

            services.AddTransient<IRedisService, RedisService>();

            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHomeSQL, HomeSQL>();

            return services;
        }
    }
}
