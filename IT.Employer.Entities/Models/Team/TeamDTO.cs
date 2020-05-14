using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Entities.Models.EmployeeN;
using System;
using System.Collections.Generic;

namespace IT.Employer.Entities.Models.Team
{
    public class TeamDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CompanyId { get; set; }

        public CompanyDTO Company { get; set; }
        public List<EmployeeDTO> Members { get; set; }
    }
}
