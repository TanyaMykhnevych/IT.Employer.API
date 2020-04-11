using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Domain.Models.User
{
    public class AppUser : IdentityUser<Guid>
    {
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}
