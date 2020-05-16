using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Team;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.TeamN
{
    public interface ITeamService
    {
        TeamDTO GetById(Guid id);
        Task<Guid> Create(TeamDTO team);
        Task Update(TeamDTO team);
        Task Delete(Guid id);
        SearchResponseDTO<TeamSearchItemDTO> SearchTeams(SearchTeamParameterDTO parameters);
    }
}
