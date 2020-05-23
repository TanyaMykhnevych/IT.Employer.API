using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using System;

namespace IT.Employer.Entities.Models.CompanyN
{
    public class SearchCompanyParameterDTO : PaginationModelDTO
    {
        public CompanyTypeDTO? Type { get; set; }
        public CompanySizeDTO? Size { get; set; }
        public string SearchTerm { get; set; }
        public Guid? CurrentUserCompanyId { get; set; }
        public bool MyCompanies { get; set; }
    }
}
