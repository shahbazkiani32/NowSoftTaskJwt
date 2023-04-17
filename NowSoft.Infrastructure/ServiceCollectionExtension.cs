
using Microsoft.Extensions.DependencyInjection;
using NowSoft.Application.Contracts;
using NowSoft.Infrastructure.Authorization;
using NowSoft.Infrastructure.Repository;
using NowSoft.Infrastructure.Service;

namespace NowSoft.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
