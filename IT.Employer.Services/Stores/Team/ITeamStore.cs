using IT.Employer.Domain.Models.TeamN;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public interface ITeamStore
    {
        Team GetById(Guid id);
        Task<Guid> Create(Team team);
        Task Update(Team team);
        Task Delete(Guid id);
    }
}
