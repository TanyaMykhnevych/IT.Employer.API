using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IT.Employer.Domain;

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
