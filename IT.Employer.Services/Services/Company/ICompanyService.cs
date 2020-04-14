using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.CompanyN
{
    public interface ICompanyService
    {
        CompanyDTO GetById(Guid id);
        Task<Guid> Create(CompanyDTO company);
        Task Update(CompanyDTO company);
        Task Delete(Guid id);
        SearchResponseDTO<CompanyDTO> SearchCompanies(SearchCompanyParameterDTO parameters);
    }
}
