using IT.Employer.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.Base
{
    public class BaseUserAccessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public BaseUserAccessController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<AppUser> GetCurrentUser()
        {
            string name = _userManager.GetUserName(User);
            return await _userManager.FindByNameAsync(name);
        }

        protected async Task<Guid?> GetCurrentUserId()
        {
            string name = _userManager.GetUserName(User);
            return (await _userManager.FindByNameAsync(name))?.Id;
        }
    }
}
