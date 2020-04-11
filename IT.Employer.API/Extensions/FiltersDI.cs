using IT.Employer.WebAPI.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Employer.WebAPI.Extensions
{
    public static class FiltersDI
    {
        public static void AddCustomFilters(this IServiceCollection services)
        {
            services.AddTransient<CustomValidateModelAttribute>();
        }
    }
}
