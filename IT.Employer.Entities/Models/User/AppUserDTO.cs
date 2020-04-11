using IT.Employer.Entities.Models.Base;

namespace IT.Employer.Entities.Models.User
{
    public class AppUserDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}
