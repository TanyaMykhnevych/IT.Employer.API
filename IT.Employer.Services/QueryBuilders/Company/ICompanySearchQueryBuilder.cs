using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Services.QueryBuilders.Base;

namespace IT.Employer.Services.QueryBuilders.CompanyN
{
    public interface ICompanySearchQueryBuilder : IQueryBuilder<Company>
    {
        ICompanySearchQueryBuilder SetBaseCompaniesInfo(bool asNoTracking = true);
        ICompanySearchQueryBuilder SetType(CompanyType? type);
        ICompanySearchQueryBuilder SetSize(CompanySize? size);
        ICompanySearchQueryBuilder SetSearchTerm(string searchterm);
    }
}
