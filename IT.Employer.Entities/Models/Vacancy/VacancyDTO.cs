using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Company;
using IT.Employer.Shared.Enums;
using System;

namespace IT.Employer.Entities.Models.Vacancy
{
    public class VacancyDTO : BaseEntityDTO
    {
        public Profession Profession { get; set; }
        public Position Position { get; set; }
        public Technology PrimaryTechnology { get; set; }
        public float ExperienceYears { get; set; }
        public Guid? CompanyId { get; set; }

        public CompanyDTO Company { get; set; }
    }
}
