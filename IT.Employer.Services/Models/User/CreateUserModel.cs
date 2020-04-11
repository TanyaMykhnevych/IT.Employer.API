using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Services.Models.User
{
    public class CreateUserModel
    {
        public string Role { get; set; }
        [EmailAddress]
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
    }
}
