using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Entities.Models.Team;
using System;
using System.Collections.Generic;

namespace IT.Employer.Entities.Models.EmployeeN
{
    public class EmployeeDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime? BirthDate { get; set; }

        public ProfessionDTO Profession { get; set; }
        public PositionDTO Position { get; set; }
        public TechnologyDTO PrimaryTechnology { get; set; }

        public float ExperienceYears { get; set; }

        public string Email { get; set; }
        public string Skype { get; set; }
        public string LinkedIn { get; set; }

        public Guid? CompanyId { get; set; }
        public Guid? TeamId { get; set; }

        public CompanyDTO Company { get; set; }
        public TeamDTO Team { get; set; }
        public List<CharacteristicDTO> Сharacteristics { get; set; }
    }
}
