using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Core.Entities;
using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Auth.Applicatoin.BusinessLogic
{
    public class RoleManagerProcessor : IRoleManagerProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RoleManagerProcessor> _logger;
        public RoleManagerProcessor(UserManager<ApplicationUser> userManager,
                    RoleManager<ApplicationRole>roleManager,ILogger<RoleManagerProcessor> logger) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task<GenericResponseClass<bool>> AssignRoleToUser(RoleAssignmentRequestDto requestDto)
        {
            _logger.LogInformation("Start AssignRoleToUser");
            var user = await _userManager.FindByIdAsync(requestDto.UserId.ToString());
            if (user == null)
            {
                _logger.LogError("User doesn't exist");
                return new GenericResponseClass<bool>
                {
                    Result = false,
                    ResponseMessage = ResponseMessages.UserNotRegistered,
                    Status = Enums.ResponseStatus.Error
                };
            }
            if(!IsRoleExistsAsync(requestDto.RoleName).Result)
            {
                _logger.LogError("Role doesn't exist");
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
                _logger.LogInformation("user already in role");
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
                _logger.LogInformation("User successfully assigned to the role");
                return new GenericResponseClass<bool>
                {
                    Result = true,
                    ResponseMessage = ResponseMessages.UserAssignedToRoleSuccessfully,
                    Status = Enums.ResponseStatus.Success
                };
            }
            else
            {
                _logger.LogError("Can't assign the user to the role");
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
