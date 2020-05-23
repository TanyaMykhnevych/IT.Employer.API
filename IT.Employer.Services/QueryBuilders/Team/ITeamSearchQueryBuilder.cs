using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Services.QueryBuilders.Base;
using System;
using System.Collections.Generic;

namespace IT.Employer.Services.QueryBuilders.TeamN
{
    public interface ITeamSearchQueryBuilder : IQueryBuilder<Team>
    {
        ITeamSearchQueryBuilder SetBaseTeamsInfo(bool asNoTracking = true, bool includeRelated = true);
        ITeamSearchQueryBuilder SetCompanyId(Guid? companyId);
        ITeamSearchQueryBuilder SetSearchTerm(string searchTerm);
        ITeamSearchQueryBuilder SetNumberOfMembers(int? from, int? to);
        ITeamSearchQueryBuilder SetTechnologies(List<Technology> technologies);
        ITeamSearchQueryBuilder SetMyTeams(bool myTeams, Guid? myCompanyId);
    }
}
