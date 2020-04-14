using IT.Employer.Services.Factories.AuthTokenFactory;
using IT.Employer.Services.QueryBuilders.CompanyN;
using IT.Employer.Services.QueryBuilders.EmployeeN;
using IT.Employer.Services.QueryBuilders.VacancyN;
using IT.Employer.Services.Services;
using IT.Employer.Services.Services.CompanyN;
using IT.Employer.Services.Services.EmployeeN;
using IT.Employer.Services.Services.TeamN;
using IT.Employer.Services.Services.UserAuthorizationService;
using IT.Employer.Services.Services.VacancyN;
using IT.Employer.Services.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IT.Employer.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            //stores
            services.AddTransient<ICompanyStore, CompanyStore>();
            services.AddTransient<IEmployeeStore, EmployeeStore>();
            services.AddTransient<ITeamStore, TeamStore>();
            services.AddTransient<IVacancyStore, VacancyStore>();

            // factories
            services.AddTransient<IAuthTokenFactory, AuthTokenFactory>();

            // serivces
            services.AddTransient<BaseAuthorizationService, AppUserAuthorizationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IVacancyService, VacancyService>();
            services.AddTransient<ITeamService, TeamService>();

            // query builders
            services.AddTransient<IEmployeeSearchQueryBuilder, EmployeeSearchQueryBuilder>();
            services.AddTransient<IVacancySearchQueryBuilder, VacancySearchQueryBuilder>();
            services.AddTransient<ICompanySearchQueryBuilder, CompanySearchQueryBuilder>();

            services.TryAddSingleton<ISystemClock, SystemClock>();
        }
    }
}
