using IT.Employer.Domain;
using IT.Employer.Domain.Models.TeamN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IT.Employer.Services.QueryBuilders.TeamN
{
    public class TeamSearchQueryBuilder : ITeamSearchQueryBuilder
    {
        private readonly ItEmployerDbContext _context;
        private IQueryable<Team> _query;

        public TeamSearchQueryBuilder(ItEmployerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Team> Build()
        {
            IQueryable<Team> resultQuery = _query;
            _query = null;
            return resultQuery;
        }

        public ITeamSearchQueryBuilder SetBaseTeamsInfo(Boolean asNoTracking = true, Boolean includeRelated = true)
        {
            _query = _context.Teams;

            if (asNoTracking) _query = _query.AsNoTracking();

            if (includeRelated)
            {
                _query = _query.Include(e => e.Company).Include(t => t.Members);
            }

            _query = _query.Where(t => t.Members.All(m => !m.Inactive));

            return this;
        }

        public ITeamSearchQueryBuilder SetCompanyId(Guid? companyId)
        {
            if (companyId.HasValue)
            {
                _query = _query.Where(e => e.CompanyId == companyId.Value);
            }

            return this;
        }

        public ITeamSearchQueryBuilder SetNumberOfMembers(int? from, int? to)
        {
            if (from.HasValue)
            {
                _query = _query.Where(e => e.Members.Count(m => !m.Inactive) >= from.Value);
            }

            if (to.HasValue)
            {
                _query = _query.Where(e => e.Members.Count(m => !m.Inactive) <= to.Value);
            }

            return this;
        }

        public ITeamSearchQueryBuilder SetSearchTerm(string searchterm)
        {
            if (!string.IsNullOrWhiteSpace(searchterm))
            {
                _query = _query.Where(e => e.Name.Contains(searchterm));
            }

            return this;
        }
    }
}
