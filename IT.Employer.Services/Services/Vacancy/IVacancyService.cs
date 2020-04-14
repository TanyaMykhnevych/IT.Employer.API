using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Vacancy;
using IT.Employer.Entities.Models.VacancyN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.VacancyN
{
    public interface IVacancyService
    {
        VacancyDTO GetById(Guid id);
        Task<Guid> Create(VacancyDTO vacancy);
        Task Update(VacancyDTO vacancy);
        Task Delete(Guid id);
        SearchResponseDTO<VacancyDTO> SearchVacancies(SearchVacancyParameterDTO parameters);
    }
}
