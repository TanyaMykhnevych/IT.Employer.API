using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;

namespace IT.Employer.Domain.Models.CompanyN
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CompanyType? Type { get; set; }
        public CompanySize? Size { get; set; }
    }
}
