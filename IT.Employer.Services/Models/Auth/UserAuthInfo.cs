using System;

namespace IT.Employer.Services.Models.Auth
{
    public class UserAuthInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
