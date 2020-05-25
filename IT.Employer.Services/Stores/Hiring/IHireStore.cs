using IT.Employer.Domain.Models.Hiring;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores.Hiring
{
    public interface IHireStore
    {
        Task<Guid> Create(Hire employee);
        Hire GetById(Guid id);
        List<Hire> GetHiresByCompanyId(Guid companyId);
        List<Hire> GetSentHiresByCompanyId(Guid companyId);
        Task Update(Hire employee);
    }
}
