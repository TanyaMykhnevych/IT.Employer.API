﻿using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Services.Exceptions.Common;
using IT.Employer.Services.QueryBuilders.EmployeeN;
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

        public EmployeeService(
            IEmployeeStore store,
            IEmployeeSearchQueryBuilder queryBuilder,
            IMapper mapper)
        {
            _store = store;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
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
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task Update(EmployeeDTO employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _store.Update(employee);
        }

        public SearchResponseDTO<EmployeeDTO> SearchEmployees(SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<Employee> result = SearchEmployeesWithQuery(parameters);
            return new SearchResponseDTO<EmployeeDTO>
            {
                Items = _mapper.Map<IEnumerable<EmployeeDTO>>(result.Items),
                TotalCount = result.TotalCount
            };
        }
        private SearchResponseDTO<Employee> SearchEmployeesWithQuery(SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Page.HasValue && parameters.Page.Value < 0 ||
                 parameters.PerPage.HasValue && parameters.PerPage.Value < 0)
            {
                throw new InvalidPaginationParametersException();
            }

            IQueryable<Employee> query = GetEmployeesSearchQuery(parameters);

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

        private IQueryable<Employee> GetEmployeesSearchQuery(SearchEmployeeParameterDTO parameters)
        {
            IQueryable<Employee> query = _queryBuilder.SetBaseEmployeesInfo()
                                                     .SetFirstName(parameters.FirstName)
                                                     .SetLastName(parameters.LastName)
                                                     .SetPosition(_mapper.Map<Position?>(parameters.Position))
                                                     .SetProfession(_mapper.Map<Profession?>(parameters.Profession))
                                                     .SetPrimaryTechnology(_mapper.Map<Technology?>(parameters.PrimaryTechnology))
                                                     .SetCompanyId(parameters.CompanyId)
                                                     .SetTeamId(parameters.TeamId)
                                                     .SetExperience(parameters.ExperienceFrom, parameters.ExperienceTo)
                                                     .Build();
            return query;
        }
    }
}
