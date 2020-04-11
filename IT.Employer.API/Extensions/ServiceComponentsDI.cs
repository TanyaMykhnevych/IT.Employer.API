using IT.Employer.Services.Factories.AuthTokenFactory;
using IT.Employer.Services.Services;
using IT.Employer.Services.Services.UserAuthorizationService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IT.Employer.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<BaseAuthorizationService, AppUserAuthorizationService>();
            services.AddTransient<IAuthTokenFactory, AuthTokenFactory>();
            services.AddTransient<IUserService, UserService>();

            services.TryAddSingleton<ISystemClock, SystemClock>();
        }
    }
}
