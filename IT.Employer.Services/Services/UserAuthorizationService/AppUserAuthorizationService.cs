﻿using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Services.Factories.AuthTokenFactory;
using IT.Employer.Services.Models.Auth;
using IT.Employer.Services.Services.CompanyN;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.UserAuthorizationService
{
    public class AppUserAuthorizationService : BaseAuthorizationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICompanyService _companyService;

        public AppUserAuthorizationService(
            IAuthTokenFactory tokenFactory,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ICompanyService companyService)
            : base(tokenFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _companyService = companyService;
        }

        public override async Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return new List<Claim> { };
            }

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
        }

        public async override Task<bool> VerifyUserAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return false;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return result.Succeeded;
        }

        public async override Task<UserAuthInfo> GetUserInfoAsync(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);

            UserAuthInfo info = new UserAuthInfo
            {
                Role = user.Role,
                UserId = user.Id,
                UserName = user.UserName,
                CompanyId = user.CompanyId,
            };

            if (user.CompanyId.HasValue)
            {
                CompanyDTO company = _companyService.GetById(user.CompanyId.Value);
                info.CompanyName = company.Name;
            }

            return info;
        }
    }
}
