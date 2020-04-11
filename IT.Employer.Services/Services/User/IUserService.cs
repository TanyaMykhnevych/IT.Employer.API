using IT.Employer.Domain.Models.User;
using IT.Employer.Services.Models.User;
using System;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> CreateUserAsync(CreateUserModel userModel);
        Task<AppUser> UpdateUserAsync(UpdateUserModel userModel);
        Task DeactivateUser(Guid userId);
        Task DeleteUser(Guid userId);
    }
}
