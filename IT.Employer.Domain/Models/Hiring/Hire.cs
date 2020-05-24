using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.TeamN;
using System;

namespace IT.Employer.Domain.Models.Hiring
{
    public class Hire : BaseEntity
    {
        public DateTime HiredFrom { get; set; }
        public DateTime HiredTo { get; set; }
        public decimal TotalHiringRate { get; set; }
        public string HireMessage { get; set; }

        public HireStatus Status { get; set; }
        public DateTime? ApproveDate { get; set; }

        public Guid HiringCompanyId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? EmployeeId { get; set; }

        public Company HiringCompany { get; set; }
        public Company Company { get; set; }
        public Team Team { get; set; }
        public Employee Employee { get; set; }
    }
}
