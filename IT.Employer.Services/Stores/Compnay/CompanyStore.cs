using IT.Employer.Domain;
using IT.Employer.Domain.Models.CompanyN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public class CompanyStore : ICompanyStore
    {
        private readonly ItEmployerDbContext _context;

        public CompanyStore(ItEmployerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Company company)
        {
            await _context.AddAsync(company);
            await _context.SaveChangesAsync();

            return company.Id;
        }

        public async Task Delete(Guid id)
        {
            Company company = _context.Companies.Find(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public Company GetById(Guid id)
        {
            return _context.Companies
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task Update(Company company)
        {
            _context.Update(company);
            await _context.SaveChangesAsync();
        }
    }
}
