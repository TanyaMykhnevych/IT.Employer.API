using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;

namespace IT.Employer.Entities.Models.CompanyN
{
    public class SearchCompanyParameterDTO : PaginationModelDTO
    {
        public CompanyTypeDTO? Type { get; set; }
        public CompanySizeDTO? Size { get; set; }
        public string SearchTerm { get; set; }
    }
}
