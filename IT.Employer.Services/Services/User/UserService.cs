using AutoMapper;
using IT.Employer.Domain.Models.User;
using IT.Employer.Services.Constants;
using IT.Employer.Services.Exceptions.User;
using IT.Employer.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<AppUser> CreateUserAsync(CreateUserModel model)
        {
            AppUser existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException();
            }

            AppUser user = _mapper.Map<AppUser>(model);
            user.IsActive = true;
            IdentityResult addUserResult = await _userManager.CreateAsync(user, model.Password);

            ValidateIdentityResult(addUserResult);

            return await GetUserByUsername(user.UserName);
        }

        public async Task<AppUser> UpdateUserAsync(UpdateUserModel model)
        {
            List<string> errors = new List<string>();
            Boolean result = ValidatePasswords(model, out errors);

            if (!result)
            {
                throw new InvalidUserPasswordException(String.Join(" ", errors));
            }

            AppUser existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null && existingUser.Id != model.Id)
            {
                throw new UsernameAlreadyTakenException();
            }

            AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            _mapper.Map(model, user);

            IdentityResult updateUserResult = await _userManager.UpdateAsync(user);
            ValidateIdentityResult(updateUserResult);

            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                IdentityResult changePasswordsResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                if (!changePasswordsResult.Succeeded)
                {
                    throw new InvalidUserPasswordException(String.Join(" ", errors));
                }
            }

            return await GetUserByUsername(user.UserName);
        }

        public async Task DeactivateUser(Guid userId)
        {
            AppUser userToDeactivate = await _userManager.FindByIdAsync(userId.ToString());
            if (userToDeactivate == null)
            {
                throw new UserNotFoundException();
            }

            userToDeactivate.IsActive = false;

            IdentityResult updateUserResult = await _userManager.UpdateAsync(userToDeactivate);
            ValidateIdentityResult(updateUserResult);
        }

        public async Task DeleteUser(Guid userId)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            IdentityResult deleteUserResult = await _userManager.DeleteAsync(userToDelete);

            ValidateIdentityResult(deleteUserResult);
        }

        public async Task SetCompany(Guid userId, Guid companyId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());

            user.CompanyId = companyId;

            await _userManager.UpdateAsync(user);
        }


        private void ValidateIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                String errorsMessage = result.Errors
                                         .Select(er => er.Description)
                                         .Aggregate((i, j) => i + ";" + j);
                throw new Exception(errorsMessage);
            }
        }

        private bool ValidatePasswords(UpdateUserModel model, out List<String> errors)
        {
            errors = new List<string>();
            if (String.IsNullOrEmpty(model.Password) &&
                String.IsNullOrEmpty(model.NewPassword) &&
                String.IsNullOrEmpty(model.ConfirmPassword))
            {
                return true;
            }

            if (String.IsNullOrEmpty(model.Password) ||
                String.IsNullOrEmpty(model.NewPassword) ||
                String.IsNullOrEmpty(model.ConfirmPassword))
            {
                errors.Add(ErrorMessagesConstants.NOT_ALL_PASS_FIELDS_FILLED);
            }

            if (!model.NewPassword.Equals(model.ConfirmPassword))
            {
                errors.Add(ErrorMessagesConstants.PASSWORDS_DO_NOT_MATCH);
            }

            return errors.Any() ? false : true;
        }
    }
}
