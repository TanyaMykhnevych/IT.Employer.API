using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.VacancyN;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Vacancy;
using IT.Employer.Entities.Models.VacancyN;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.QueryBuilders.VacancyN;
using IT.Employer.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.VacancyN
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyStore _store;
        private readonly IVacancySearchQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public VacancyService(
            IVacancyStore store,
            IVacancySearchQueryBuilder queryBuilder,
            IMapper mapper)
        {
            _store = store;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
        }

        public async Task<Guid> Create(VacancyDTO vacancyDto)
        {
            Vacancy vacancy = _mapper.Map<Vacancy>(vacancyDto);
            return await _store.Create(vacancy);
        }

        public async Task Delete(Guid id)
        {
            await _store.Delete(id);
        }

        public VacancyDTO GetById(Guid id)
        {
            Vacancy vacancy = _store.GetById(id);
            return _mapper.Map<VacancyDTO>(vacancy);
        }

        public async Task Update(VacancyDTO VacancyDto)
        {
            Vacancy Vacancy = _mapper.Map<Vacancy>(VacancyDto);
            await _store.Update(Vacancy);
        }

        public SearchResponseDTO<VacancyDTO> SearchVacancies(SearchVacancyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Vacancy> result = SearchVacanciesWithQuery(parameters);
            return new SearchResponseDTO<VacancyDTO>
            {
                Items = _mapper.Map<IEnumerable<VacancyDTO>>(result.Items),
                TotalCount = result.TotalCount
            };
        }

        private SearchResponseDTO<Vacancy> SearchVacanciesWithQuery(SearchVacancyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Page.HasValue && parameters.Page.Value < 0 ||
                 parameters.PerPage.HasValue && parameters.PerPage.Value < 0)
            {
                throw new InvalidPaginationParametersException();
            }

            IQueryable<Vacancy> query = GetVacanciesSearchQuery(parameters);

            int totalCount = query.Count();

            if (parameters.Page.HasValue && parameters.Page != 0 && parameters.PerPage.HasValue)
            {
                query = query.Skip((parameters.Page.Value - 1) * parameters.PerPage.Value);
            }

            if (parameters.PerPage.HasValue)
            {
                query = query.Take(parameters.PerPage.Value);
            }

            List<Vacancy> vacancies = query.ToList();

            return new SearchResponseDTO<Vacancy>
            {
                Items = vacancies,
                TotalCount = totalCount
            };
        }

        private IQueryable<Vacancy> GetVacanciesSearchQuery(SearchVacancyParameterDTO parameters)
        {
            IQueryable<Vacancy> query = _queryBuilder.SetBaseVacanciesInfo()
                                         .SetPosition(_mapper.Map<Position?>(parameters.Position))
                                         .SetProfession(_mapper.Map<Profession?>(parameters.Profession))
                                         .SetPrimaryTechnology(_mapper.Map<Technology?>(parameters.PrimaryTechnology))
                                         .SetExperience(parameters.ExperienceFrom, parameters.ExperienceTo)
                                         .SetMyVacancies(parameters.MyVacancies, parameters.UserId)
                                         .Build();
            return query;
        }
    }
}
