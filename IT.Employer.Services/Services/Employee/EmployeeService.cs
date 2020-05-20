using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.Extensions;
using IT.Employer.Services.QueryBuilders.EmployeeN;
using IT.Employer.Services.Services.PricePolicies;
using IT.Employer.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.EmployeeN
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeStore _store;
        private readonly IEmployeeSearchQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;
        private readonly IPricePolicyService _pricePolicyService;

        public EmployeeService(
            IEmployeeStore store,
            IEmployeeSearchQueryBuilder queryBuilder,
            IMapper mapper,
            IPricePolicyService pricePolicyService)
        {
            _store = store;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
            _pricePolicyService = pricePolicyService;
        }

        public async Task<Guid> Create(EmployeeDTO employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            return await _store.Create(employee);
        }

        public async Task Delete(Guid id)
        {
            await _store.Delete(id);
        }

        public EmployeeDTO GetById(Guid id)
        {
            Employee employee = _store.GetById(id);
            EmployeeDTO result = _mapper.Map<EmployeeDTO>(employee);
            SetHiringRate(result, 1);

            return result;
        }

        public async Task Update(EmployeeDTO employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _store.Update(employee);
        }

        public SearchResponseDTO<EmployeeDTO> SearchEmployees(SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Employee> result = SearchEmployeesWithQuery(parameters, GetEmployeesSearchQuery);
            return new SearchResponseDTO<EmployeeDTO>
            {
                Items = _mapper.Map<IEnumerable<EmployeeDTO>>(result.Items).Do(employee => SetHiringRate(employee, 1)),
                TotalCount = result.TotalCount
            };
        }

        public SearchResponseDTO<EmployeeDTO> SearchSingleActiveEmployees(SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Employee> result = SearchEmployeesWithQuery(parameters, GetSingleActiveEmployeesSearchQuery);

            return new SearchResponseDTO<EmployeeDTO>
            {
                Items = _mapper.Map<IEnumerable<EmployeeDTO>>(result.Items).Do(employee => SetHiringRate(employee, 1)),
                TotalCount = result.TotalCount
            };
        }

        private SearchResponseDTO<Employee> SearchEmployeesWithQuery(
            SearchEmployeeParameterDTO parameters,
            Func<SearchEmployeeParameterDTO, IQueryable<Employee>> getEmployeeFunction)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Page.HasValue && parameters.Page.Value < 0 ||
                 parameters.PerPage.HasValue && parameters.PerPage.Value < 0)
            {
                throw new InvalidPaginationParametersException();
            }

            IQueryable<Employee> query = getEmployeeFunction(parameters);

            int totalCount = query.Count();

            if (parameters.Page.HasValue && parameters.Page != 0 && parameters.PerPage.HasValue)
            {
                query = query.Skip((parameters.Page.Value - 1) * parameters.PerPage.Value);
            }

            if (parameters.PerPage.HasValue)
            {
                query = query.Take(parameters.PerPage.Value);
            }

            List<Employee> employees = query.ToList();

            return new SearchResponseDTO<Employee>
            {
                Items = employees,
                TotalCount = totalCount
            };
        }

        private IQueryable<Employee> GetSingleActiveEmployeesSearchQuery(SearchEmployeeParameterDTO parameters)
        {
            IQueryable<Employee> query = GetBaseBuilder(_queryBuilder, parameters)
                                        .OnlyActive()
                                        .WithoutTeam()
                                        .Build();
            return query;
        }

        private IQueryable<Employee> GetEmployeesSearchQuery(SearchEmployeeParameterDTO parameters)
        {
            IQueryable<Employee> query = GetBaseBuilder(_queryBuilder, parameters).Build();

            return query;
        }

        private IEmployeeSearchQueryBuilder GetBaseBuilder(IEmployeeSearchQueryBuilder queryBuilder, SearchEmployeeParameterDTO parameters)
        {
            return _queryBuilder.SetBaseEmployeesInfo()
                                .SetFirstName(parameters.FirstName)
                                .SetLastName(parameters.LastName)
                                .SetPosition(_mapper.Map<Position?>(parameters.Position))
                                .SetProfession(_mapper.Map<Profession?>(parameters.Profession))
                                .SetPrimaryTechnology(_mapper.Map<Technology?>(parameters.PrimaryTechnology))
                                .SetCompanyId(parameters.CompanyId)
                                .SetTeamId(parameters.TeamId)
                                .SetExperience(parameters.ExperienceFrom, parameters.ExperienceTo)
                                .SetHiringHourRate(
                                    parameters.MinHiringHourRate.HasValue ? GetInitialRate(parameters.MinHiringHourRate.Value) : (decimal?)null,
                                    parameters.MaxHiringHourRate.HasValue ? GetInitialRate(parameters.MaxHiringHourRate.Value) : (decimal?)null
                                );
        }

        private void SetHiringRate(EmployeeDTO employee, int teamSize)
        {
            if (employee != null)
            {
                employee.HiringHourRate = _pricePolicyService.CalculateHiringHourPrice(employee.HourRate, teamSize);
            }
        }

        private decimal GetInitialRate(decimal hiringHourRate)
        {
            return _pricePolicyService.CalculateInitialHourPrice(hiringHourRate, 1);
        }
    }
}
