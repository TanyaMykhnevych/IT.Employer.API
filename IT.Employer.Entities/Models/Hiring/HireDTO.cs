using IT.Employer.Domain.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Entities.Models.Team;
using System;

namespace IT.Employer.Entities.Models.Hiring
{
    public class HireDTO : BaseEntityDTO
    {
        public DateTime HiredFrom { get; set; }
        public DateTime HiredTo { get; set; }
        public decimal TotalHiringRate { get; set; }
        public string HireMessage { get; set; }

        public HireStatusDTO Status { get; set; }
        public DateTime? ApproveDate { get; set; }

        public Guid HiringCompanyId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? EmployeeId { get; set; }

        public CompanyDTO HiringCompany { get; set; }
        public CompanyDTO Company { get; set; }
        public TeamDTO Team { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
