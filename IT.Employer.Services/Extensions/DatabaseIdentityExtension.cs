using IT.Employer.Domain;
using IT.Employer.Domain.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IT.Employer.Services.Extensions
{
    public static class DatabaseIdentityExtension
    {
        public static void SetupIdentity(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IdentityBuilder builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, typeof(ITEmployerRole), builder.Services);
            builder.AddEntityFrameworkStores<ItEmployerDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }
    }
}
