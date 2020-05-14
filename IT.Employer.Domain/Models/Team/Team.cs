using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.EmployeeN;
using System;
using System.Collections.Generic;

namespace IT.Employer.Domain.Models.TeamN
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CompanyId { get; set; }

        public Company Company { get; set; }
        public List<Employee> Members { get; set; }
    }
}
