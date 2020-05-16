using System;

namespace IT.Employer.Entities.Models.Team
{
    public class TeamSearchItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfMembers { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
