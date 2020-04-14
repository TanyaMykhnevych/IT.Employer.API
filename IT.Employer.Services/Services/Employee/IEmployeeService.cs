using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.EmployeeN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.EmployeeN
{
    public interface IEmployeeService
    {
        EmployeeDTO GetById(Guid id);
        Task<Guid> Create(EmployeeDTO employee);
        Task Update(EmployeeDTO employee);
        Task Delete(Guid id);
        SearchResponseDTO<EmployeeDTO> SearchEmployees(SearchEmployeeParameterDTO parameters);
    }
}
