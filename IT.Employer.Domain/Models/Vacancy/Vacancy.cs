using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.User;
using System;

namespace IT.Employer.Domain.Models.VacancyN
{
    public class Vacancy : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Profession Profession { get; set; }
        public Position Position { get; set; }
        public Technology PrimaryTechnology { get; set; }
        public float ExperienceYears { get; set; }
        public Guid? CompanyId { get; set; }
        public string Image { get; set; }
        public Guid? UserId { get; set; }

        public Company Company { get; set; }
        public AppUser User { get; set; }
    }
}
