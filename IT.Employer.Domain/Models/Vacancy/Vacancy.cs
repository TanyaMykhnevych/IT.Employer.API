using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Shared.Enums;
using System;

namespace IT.Employer.Domain.Models.Vacancy
{
    public class Vacancy : BaseEntity
    {
        public Profession Profession { get; set; }
        public Position Position { get; set; }
        public Technology PrimaryTechnology { get; set; }
        public float ExperienceYears { get; set; }
        public Guid? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
