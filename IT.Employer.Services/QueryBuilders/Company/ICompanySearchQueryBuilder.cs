using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Services.QueryBuilders.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT.Employer.Services.QueryBuilders.CompanyN
{
    public interface ICompanySearchQueryBuilder : IQueryBuilder<Company>
    {
        ICompanySearchQueryBuilder SetBaseCompaniesInfo(bool asNoTracking = true);
        ICompanySearchQueryBuilder SetType(CompanyType? type);
        ICompanySearchQueryBuilder SetSize(CompanySize? size);
    }
}
