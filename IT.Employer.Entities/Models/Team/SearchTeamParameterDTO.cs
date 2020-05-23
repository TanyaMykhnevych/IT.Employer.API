using IT.Employer.Entities.Models.Base;
using System;

namespace IT.Employer.Entities.Models.Team
{
    public class SearchTeamParameterDTO : PaginationModelDTO
    {
        public Guid? CompanyId { get; set; }
        public Guid? CurrentUserCompanyId { get; set; }
        public string SearchTerm { get; set; }
        public int? MinNumberOfMembers { get; set; }
        public int? MaxNumberOfMembers { get; set; }
        public string Technologies { get; set; }
        public bool MyTeams { get; set; }
    }
}
