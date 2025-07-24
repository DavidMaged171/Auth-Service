using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Core.Entities;
using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Applicatoin.BusinessLogic
{
    public class RoleManagerProcessor : IRoleManagerProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleManagerProcessor(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole>roleManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<GenericResponseClass<bool>> AssignRoleToUser(RoleAssignmentRequestDto requestDto)
        {
            var user = await _userManager.FindByIdAsync(requestDto.UserId.ToString());
            if (user == null)
            {
                return new GenericResponseClass<bool>
                {
                    Result = false,
                    ResponseMessage = ResponseMessages.UserNotRegistered,
                    Status = Enums.ResponseStatus.Error
                };
            }
            if(!IsRoleExistsAsync(requestDto.RoleName).Result)
            {
                return new GenericResponseClass<bool>
                {
                    Result = false,
                    ResponseMessage = ResponseMessages.RoleNotFound,
                    Status = Enums.ResponseStatus.Error
                };
            }
            var isInRole = await _userManager.IsInRoleAsync(user, requestDto.RoleName);
            if (isInRole)
            {
                return new GenericResponseClass<bool>
                {
                    Result = true,
                    ResponseMessage = ResponseMessages.UserAlreadyInRole,
                    Status = Enums.ResponseStatus.Error
                };
            }
            var result= await _userManager.AddToRoleAsync(user, requestDto.RoleName);
            if (result.Succeeded)
            {
                return new GenericResponseClass<bool>
                {
                    Result = true,
                    ResponseMessage = ResponseMessages.UserAssignedToRoleSuccessfully,
                    Status = Enums.ResponseStatus.Success
                };
            }
            else
            {
                return new GenericResponseClass<bool>
                {
                    Result = false,
                    ResponseMessage = ResponseMessages.GenericError,
                    Status = Enums.ResponseStatus.Error
                };
            }
        }
        public async Task<bool> IsRoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
