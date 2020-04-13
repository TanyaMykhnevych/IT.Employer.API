using IT.Employer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Employer.Services.Extensions
{
    public static class DbContextProviderExtension
    {
        public static void AddITEmployerDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ItEmployerDbContext>(provider => provider.UseSqlServer(connection));
        }
    }
}
