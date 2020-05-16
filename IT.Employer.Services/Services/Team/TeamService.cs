using AutoMapper;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Team;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.QueryBuilders.TeamN;
using IT.Employer.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.TeamN
{
    public class TeamService : ITeamService
    {
        private readonly ITeamStore _store;
        private readonly IMapper _mapper;
        private readonly ITeamSearchQueryBuilder _queryBuilder;

        public TeamService(
            ITeamStore store,
            IMapper mapper,
            ITeamSearchQueryBuilder queryBuilder)
        {
            _store = store;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
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


        public SearchResponseDTO<TeamSearchItemDTO> SearchTeams(SearchTeamParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Team> result = SearchTeamsWithQuery(parameters);

            var items = result.Items.Select(i => new TeamSearchItemDTO
            {
                Name = i.Name,
                Description = i.Description,
                CompanyName = i.Company?.Name,
                CreatedOn = i.CreatedOn,
                NumberOfMembers = i.Members.Count()
            });

            return new SearchResponseDTO<TeamSearchItemDTO>
            {
                Items = items,
                TotalCount = result.TotalCount
            };
        }

        public SearchResponseDTO<Team> SearchTeamsWithQuery(SearchTeamParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Page.HasValue && parameters.Page.Value < 0 ||
                 parameters.PerPage.HasValue && parameters.PerPage.Value < 0)
            {
                throw new InvalidPaginationParametersException();
            }

            IQueryable<Team> query = GetTeamsSearchQuery(parameters);

            int totalCount = query.Count();

            if (parameters.Page.HasValue && parameters.Page != 0 && parameters.PerPage.HasValue)
            {
                query = query.Skip((parameters.Page.Value - 1) * parameters.PerPage.Value);
            }

            if (parameters.PerPage.HasValue)
            {
                query = query.Take(parameters.PerPage.Value);
            }

            List<Team> teams = query.ToList();

            return new SearchResponseDTO<Team>
            {
                Items = teams,
                TotalCount = totalCount
            };
        }

        private IQueryable<Team> GetTeamsSearchQuery(SearchTeamParameterDTO parameters)
        {
            IQueryable<Team> query = _queryBuilder.SetBaseTeamsInfo()
                                         .SetCompanyId(parameters.CompanyId)
                                         .SetSearchTerm(parameters.SearchTerm)
                                         .SetNumberOfMembers(parameters.MinNumberOfMembers, parameters.MaxNumberOfMembers)
                                         .Build();
            return query;
        }
    }
}
