using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Services.QueryBuilders.Base;
using System;

namespace IT.Employer.Services.QueryBuilders.TeamN
{
    public interface ITeamSearchQueryBuilder : IQueryBuilder<Team>
    {
        ITeamSearchQueryBuilder SetBaseTeamsInfo(bool asNoTracking = true, bool includeRelated = true);
        ITeamSearchQueryBuilder SetCompanyId(Guid? companyId);
        ITeamSearchQueryBuilder SetSearchTerm(string searchTerm);
        ITeamSearchQueryBuilder SetNumberOfMembers(int? from, int? to);
    }
}
