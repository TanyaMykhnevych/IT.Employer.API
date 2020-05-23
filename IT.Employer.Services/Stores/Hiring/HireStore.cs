using IT.Employer.Domain;
using IT.Employer.Domain.Models.Hiring;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores.Hiring
{
    public class HireStore : IHireStore
    {
        private readonly ItEmployerDbContext _context;

        public HireStore(ItEmployerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Hire employee)
        {
            await _context.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public Hire GetById(Guid id)
        {
            return _context.Hires
                .AsNoTracking()
                .Include(e => e.HiringCompany)
                .Include(e => e.Company)
                .Include(e => e.Employee)
                .Include(e => e.Team)
                .ThenInclude(t => t.Members)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Hire> GetHiresByCompanyId(Guid companyId)
        {
            return _context.Hires
               .AsNoTracking()
               .Include(e => e.HiringCompany)
               .Include(e => e.Company)
               .Include(e => e.Employee)
               .Include(e => e.Team)
               .ThenInclude(t => t.Members)
               .Where(c => c.CompanyId == companyId)
               .ToList();
        }

        public async Task Update(Hire employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
