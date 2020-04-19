using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;

namespace IT.Employer.Entities.Models.CompanyN
{
    public class CompanyDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CompanyTypeDTO? Type { get; set; }
        public CompanySizeDTO? Size { get; set; }
    }
}
