using IT.Employer.Domain.Models.Vacancy;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public interface IVacancyStore
    {
        Vacancy GetById(Guid id);
        Task<Guid> Create(Vacancy vacancy);
        Task Update(Vacancy vacancy);
        Task Delete(Guid id);
    }
}
