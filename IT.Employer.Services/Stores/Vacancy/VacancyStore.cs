using IT.Employer.Domain;
using IT.Employer.Domain.Models.Vacancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public class VacancyStore : IVacancyStore
    {
        private readonly ItEmployerDbContext _context;

        public VacancyStore(ItEmployerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Vacancy vacancy)
        {
            await _context.AddAsync(vacancy);
            await _context.SaveChangesAsync();

            return vacancy.Id;
        }

        public async Task Delete(Guid id)
        {
            Vacancy vacancy = _context.Vacancies.Find(id);
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
        }

        public Vacancy GetById(Guid id)
        {
            return _context.Vacancies
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task Update(Vacancy vacancy)
        {
            _context.Update(vacancy);
            await _context.SaveChangesAsync();
        }
    }
}
