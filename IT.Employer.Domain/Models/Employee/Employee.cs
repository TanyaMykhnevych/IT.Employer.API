using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.TeamN;
using System;
using System.Collections.Generic;

namespace IT.Employer.Domain.Models.EmployeeN
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime? BirthDate { get; set; }

        public Profession Profession { get; set; }
        public Position Position { get; set; }
        public Technology PrimaryTechnology { get; set; }

        public string Сharacteristic { get; set; }
        public float ExperienceYears { get; set; }

        public string Email { get; set; }
        public string Skype { get; set; }
        public string LinkedIn { get; set; }

        public Guid? CompanyId { get; set; }
        public Guid? TeamId { get; set; }

        public Company Company { get; set; }
        public Team Team { get; set; }
        public List<Characteristic> Characteristics { get; set; }
    }
}
