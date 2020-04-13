﻿using IT.Employer.Domain;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IT.Employer.Services.QueryBuilders.EmployeeN
{
    public class EmployeeSearchQueryBuilder : IEmployeeSearchQueryBuilder
    {
        private readonly ItEmployerDbContext _context;
        private IQueryable<Employee> _query;

        public EmployeeSearchQueryBuilder(ItEmployerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> Build()
        {
            IQueryable<Employee> resultQuery = _query;
            _query = null;
            return resultQuery;
        }

        public IEmployeeSearchQueryBuilder SetBaseEmployeesInfo(Boolean asNoTracking = true, Boolean includeRelated = false)
        {
            _query = _context.Employees;

            if (asNoTracking) _query = _query.AsNoTracking();

            if (includeRelated)
            {
                _query = _query.Include(e => e.Characteristics)
                    .Include(e => e.Company)
                    .Include(e => e.Team);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetCompanyId(Guid? companyId)
        {
            if (companyId.HasValue)
            {
                _query = _query.Where(e => e.CompanyId == companyId.Value);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetExperience(int? from, int? to)
        {
            if (from.HasValue)
            {
                _query = _query.Where(e => e.ExperienceYears >= from.Value);
            }

            if (to.HasValue)
            {
                _query = _query.Where(e => e.ExperienceYears <= to.Value);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetFirstName(string firstName)
        {
            if (firstName != null)
            {
                _query = _query.Where(e => String.Equals(e.FirstName, firstName, StringComparison.CurrentCulture));
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetLastName(string lastName)
        {
            if (lastName != null)
            {
                _query = _query.Where(e => String.Equals(e.LastName, lastName, StringComparison.CurrentCulture));
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetPosition(Position? position)
        {
            if (position.HasValue)
            {
                _query = _query.Where(e => e.Position == position.Value);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetPrimaryTechnology(Technology? technology)
        {
            if (technology.HasValue)
            {
                _query = _query.Where(e => e.PrimaryTechnology == technology.Value);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetProfession(Profession? profession)
        {
            if (profession.HasValue)
            {
                _query = _query.Where(e => e.Profession == profession.Value);
            }

            return this;
        }

        public IEmployeeSearchQueryBuilder SetTeamId(Guid? teamId)
        {
            if (teamId.HasValue)
            {
                _query = _query.Where(e => e.TeamId == teamId.Value);
            }

            return this;
        }
    }
}