using IT.Employer.Domain;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.CompanyN;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IT.Employer.Services.QueryBuilders.CompanyN
{
    public class CompanySearchQueryBuilder : ICompanySearchQueryBuilder
    {
        private readonly ItEmployerDbContext _context;
        private IQueryable<Company> _query;

        public CompanySearchQueryBuilder(ItEmployerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Company> Build()
        {
            IQueryable<Company> resultQuery = _query;
            _query = null;
            return resultQuery;
        }

        public ICompanySearchQueryBuilder SetBaseCompaniesInfo(bool asNoTracking = true)
        {
            _query = _context.Companies;

            if (asNoTracking) _query = _query.AsNoTracking();

            return this;
        }

        public ICompanySearchQueryBuilder SetSize(CompanySize? size)
        {
            if (size.HasValue)
            {
                _query = _query.Where(e => e.Size == size.Value);
            }

            return this;
        }

        public ICompanySearchQueryBuilder SetType(CompanyType? type)
        {
            if (type.HasValue)
            {
                _query = _query.Where(e => e.Type == type.Value);
            }

            return this;
        }
    }
}
