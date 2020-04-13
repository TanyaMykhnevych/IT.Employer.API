using IT.Employer.Domain;
using IT.Employer.Domain.Models.EmployeeN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public class EmployeeStore : IEmployeeStore
    {
        private readonly ItEmployerDbContext _context;

        public EmployeeStore(ItEmployerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Employee employee)
        {
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task Delete(Guid id)
        {
            Employee employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public Employee GetById(Guid id)
        {
            return _context.Employees
                .AsNoTracking()
                .Include(e => e.Characteristics)
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task Update(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
