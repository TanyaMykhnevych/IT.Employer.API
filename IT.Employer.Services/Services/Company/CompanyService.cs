using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.QueryBuilders.CompanyN;
using IT.Employer.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.CompanyN
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyStore _store;
        private readonly ICompanySearchQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CompanyService(
            ICompanyStore store,
            ICompanySearchQueryBuilder queryBuilder,
            IMapper mapper,
            IUserService userService)
        {
            _store = store;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Guid> Create(CompanyDTO companyDto, Guid userId)
        {
            Company company = _mapper.Map<Company>(companyDto);

            Guid companyId = await _store.Create(company);

            await _userService.SetCompany(userId, companyId);

            return companyId;
        }

        public async Task Delete(Guid id)
        {
            await _store.Delete(id);
        }

        public CompanyDTO GetById(Guid id)
        {
            Company company = _store.GetById(id);
            return _mapper.Map<CompanyDTO>(company);
        }

        public async Task Update(CompanyDTO companyDto)
        {
            Company company = _mapper.Map<Company>(companyDto);
            await _store.Update(company);
        }

        public SearchResponseDTO<CompanyDTO> SearchCompanies(SearchCompanyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Company> result = SearchCompaniesWithQuery(parameters);
            return new SearchResponseDTO<CompanyDTO>
            {
                Items = _mapper.Map<IEnumerable<CompanyDTO>>(result.Items),
                TotalCount = result.TotalCount
            };
        }

        private SearchResponseDTO<Company> SearchCompaniesWithQuery(SearchCompanyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Page.HasValue && parameters.Page.Value < 0 ||
                 parameters.PerPage.HasValue && parameters.PerPage.Value < 0)
            {
                throw new InvalidPaginationParametersException();
            }

            IQueryable<Company> query = GetCompaniesSearchQuery(parameters);

            int totalCount = query.Count();

            if (parameters.Page.HasValue && parameters.Page != 0 && parameters.PerPage.HasValue)
            {
                query = query.Skip((parameters.Page.Value - 1) * parameters.PerPage.Value);
            }

            if (parameters.PerPage.HasValue)
            {
                query = query.Take(parameters.PerPage.Value);
            }

            List<Company> companies = query.ToList();

            return new SearchResponseDTO<Company>
            {
                Items = companies,
                TotalCount = totalCount
            };
        }

        private IQueryable<Company> GetCompaniesSearchQuery(SearchCompanyParameterDTO parameters)
        {
            IQueryable<Company> query = _queryBuilder.SetBaseCompaniesInfo()
                                                     .SetType(_mapper.Map<CompanyType?>(parameters.Type))
                                                     .SetSize(_mapper.Map<CompanySize?>(parameters.Size))
                                                     .SetSearchTerm(parameters.SearchTerm)
                                                     .Build();
            return query;
        }
    }
}
