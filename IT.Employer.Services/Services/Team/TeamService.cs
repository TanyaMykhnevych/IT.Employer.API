using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Entities.Models.Team;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.QueryBuilders.TeamN;
using IT.Employer.Services.Services.PricePolicies;
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
        private readonly IPricePolicyService _pricePolicyService;

        public TeamService(
            ITeamStore store,
            IMapper mapper,
            ITeamSearchQueryBuilder queryBuilder,
            IPricePolicyService pricePolicyService)
        {
            _store = store;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
            _pricePolicyService = pricePolicyService;
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
            TeamDTO teamDTO = _mapper.Map<TeamDTO>(team);

            SetMembersHiringRate(teamDTO.Members);

            return teamDTO;
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
                Id = i.Id.ToString(),
                Name = i.Name,
                Description = i.Description,
                CompanyName = i.Company?.Name,
                CreatedOn = i.CreatedOn,
                NumberOfMembers = i.Members.Count(),
                HourHiringRate = GetTeamHiringRate(i.Members),
                Technologies = _mapper.Map<IEnumerable<TechnologyDTO>>(i.Members.Select(m => m.PrimaryTechnology).Distinct()),
            });

            return new SearchResponseDTO<TeamSearchItemDTO>
            {
                Items = items,
                TotalCount = result.TotalCount
            };
        }

        private SearchResponseDTO<Team> SearchTeamsWithQuery(SearchTeamParameterDTO parameters)
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
            List<Technology> technologies = string.IsNullOrEmpty(parameters.Technologies) ?
                new List<Technology>() :
                parameters.Technologies.Split(',').Select(t => (Technology)Convert.ToInt32(t)).ToList();

            IQueryable<Team> query = _queryBuilder.SetBaseTeamsInfo()
                                         .SetCompanyId(parameters.CompanyId)
                                         .SetSearchTerm(parameters.SearchTerm)
                                         .SetTechnologies(technologies)
                                         .SetNumberOfMembers(parameters.MinNumberOfMembers, parameters.MaxNumberOfMembers)
                                         .Build();
            return query;
        }

        private void SetMembersHiringRate(ICollection<EmployeeDTO> employees)
        {
            if (employees == null)
            {
                return;
            }

            foreach (EmployeeDTO employee in employees)
            {
                employee.HiringHourRate = _pricePolicyService.CalculateHiringHourPrice(employee.HourRate, employees.Count);
            }
        }

        private decimal GetTeamHiringRate(ICollection<Employee> employees)
        {
            decimal totalEmployeesRate = employees.Sum(e => e.HourRate);

            return _pricePolicyService.CalculateHiringHourPrice(totalEmployeesRate, employees.Count);
        }
    }
}
