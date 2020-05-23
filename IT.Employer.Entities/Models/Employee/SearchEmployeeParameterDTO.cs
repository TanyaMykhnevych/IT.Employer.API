using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using System;

namespace IT.Employer.Entities.Models.EmployeeN
{
    public class SearchEmployeeParameterDTO : PaginationModelDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ProfessionDTO? Profession { get; set; }
        public PositionDTO? Position { get; set; }
        public TechnologyDTO? PrimaryTechnology { get; set; }

        public int? ExperienceFrom { get; set; }
        public int? ExperienceTo { get; set; }

        public int? MinHiringHourRate { get; set; }
        public int? MaxHiringHourRate { get; set; }

        public Guid? CompanyId { get; set; }
        public Guid? CurrentUserCompanyId { get; set; }
        public Guid? TeamId { get; set; }
        public bool MyEmployees { get; set; }
    }
}
