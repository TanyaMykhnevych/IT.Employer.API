using IT.Employer.Entities.Enums;
using System;
using System.Collections.Generic;

namespace IT.Employer.Entities.Models.Team
{
    public class TeamSearchItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfMembers { get; set; }
        public string CompanyName { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal HourHiringRate { get; set; }
        public IEnumerable<TechnologyDTO> Technologies { get; set; }
    }
}
