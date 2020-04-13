using IT.Employer.Domain.Models.EmployeeN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public interface IEmployeeStore
    {
        Employee GetById(Guid id);
        Task<Guid> Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(Guid id);
    }
}
