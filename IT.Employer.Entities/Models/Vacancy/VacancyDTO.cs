using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Company;
using System;

namespace IT.Employer.Entities.Models.Vacancy
{
    public class VacancyDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProfessionDTO Profession { get; set; }
        public PositionDTO Position { get; set; }
        public TechnologyDTO PrimaryTechnology { get; set; }
        public float ExperienceYears { get; set; }
        public Guid? CompanyId { get; set; }

        public CompanyDTO Company { get; set; }
    }
}
