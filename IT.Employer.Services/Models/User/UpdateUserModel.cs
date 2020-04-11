using System;
using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Services.Models.User
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Role { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        [MinLength(6)]
        public string NewPassword { get; set; }
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
    }
}
