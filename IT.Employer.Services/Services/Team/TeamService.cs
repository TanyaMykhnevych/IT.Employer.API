using AutoMapper;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Entities.Models.Team;
using IT.Employer.Services.Stores;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.TeamN
{
    public class TeamService : ITeamService
    {
        private readonly ITeamStore _store;
        private readonly IMapper _mapper;

        public TeamService(
            ITeamStore store,
            IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<Guid> Create(TeamDTO teamDto)
        {
            Team team = _mapper.Map<Team>(teamDto);
            return await _store.Create(team);
        }

        public async Task Delete(Guid id)
        {
            await _store.Delete(id);
        }

        public TeamDTO GetById(Guid id)
        {
            Team team = _store.GetById(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task Update(TeamDTO teamDto)
        {
            Team team = _mapper.Map<Team>(teamDto);
            await _store.Update(team);
        }
    }
}
