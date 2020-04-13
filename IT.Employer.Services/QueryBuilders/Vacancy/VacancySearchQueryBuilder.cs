using IT.Employer.Domain;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Vacancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT.Employer.Services.QueryBuilders.VacancyN
{
    public class VacancySearchQueryBuilder : IVacancySearchQueryBuilder
    {
        private readonly ItEmployerDbContext _context;
        private IQueryable<Vacancy> _query;

        public VacancySearchQueryBuilder(ItEmployerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Vacancy> Build()
        {
            IQueryable<Vacancy> resultQuery = _query;
            _query = null;
            return resultQuery;
        }

        public IVacancySearchQueryBuilder SetBaseVacanciesInfo(Boolean asNoTracking = true, Boolean includeRelated = false)
        {
            _query = _context.Vacancies;

            if (asNoTracking) _query = _query.AsNoTracking();

            if (includeRelated)
            {
                _query = _query.Include(e => e.Company);
            }

            return this;
        }

        public IVacancySearchQueryBuilder SetCompanyId(Guid? companyId)
        {
            if (companyId.HasValue)
            {
                _query = _query.Where(e => e.CompanyId == companyId.Value);
            }

            return this;
        }

        public IVacancySearchQueryBuilder SetExperience(int? from, int? to)
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

        public IVacancySearchQueryBuilder SetPosition(Position? position)
        {
            if (position.HasValue)
            {
                _query = _query.Where(e => e.Position == position.Value);
            }

            return this;
        }

        public IVacancySearchQueryBuilder SetPrimaryTechnology(Technology? technology)
        {
            if (technology.HasValue)
            {
                _query = _query.Where(e => e.PrimaryTechnology == technology.Value);
            }

            return this;
        }

        public IVacancySearchQueryBuilder SetProfession(Profession? profession)
        {
            if (profession.HasValue)
            {
                _query = _query.Where(e => e.Profession == profession.Value);
            }

            return this;
        }
    }
}
