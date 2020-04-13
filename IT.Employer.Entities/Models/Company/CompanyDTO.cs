using IT.Employer.Entities.Models.Base;

namespace IT.Employer.Entities.Models.Company
{
    public class CompanyDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
