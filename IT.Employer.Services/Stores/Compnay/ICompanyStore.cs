using IT.Employer.Domain.Models.CompanyN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public interface ICompanyStore
    {
        Company GetById(Guid id);
        Task<Guid> Create(Company company);
        Task Update(Company company);
        Task Delete(Guid id);
    }
}
