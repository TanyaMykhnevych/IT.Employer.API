using IT.Employer.Entities.Models.Hiring;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.Hiring
{
    public interface IHireService
    {
        HireDTO GetHireById(Guid id);
        List<HireDTO> GetCompanyOffers(Guid companyId);
        List<HireDTO> GetCompanySentOffers(Guid companyId);
        Task<Guid> CreateHire(HireDTO hire);
        Task ApproveHire(Guid id);
        Task DeclineHire(Guid id);
    }
}
