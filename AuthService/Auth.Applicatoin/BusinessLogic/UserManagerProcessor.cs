using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Applicatoin.BusinessLogic
{
    public class UserManagerProcessor:IUserManagerProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagerProcessor> _logger;
        public UserManagerProcessor(UserManager<ApplicationUser> userManager,ILogger<UserManagerProcessor>logger) 
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<GenericResponseClass<bool>> DeleteUser(int userId)
        {
            _logger.LogInformation("Start DeleteUser");
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted == false);
            if (user == null)
            {
                _logger.LogError("User doesn't exists");
                return new GenericResponseClass<bool>
                {
                    Result = false,
                    ResponseMessage = ResponseMessages.UserNotFound,
                    Status = Enums.ResponseStatus.Failed
                };
            }
            else
            {
                user.IsDeleted = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("user deleted successfully");
                    return new GenericResponseClass<bool>
                    {
                        Result = true,
                        ResponseMessage = ResponseMessages.UserDeletedSsuccessfully,
                        Status = Enums.ResponseStatus.Success
                    };
                }
                else
                {
                    _logger.LogError("Error while delete user");
                    return new GenericResponseClass<bool>
                    {
                        Result = false,
                        ResponseMessage = ResponseMessages.GenericError,
                        Status = Enums.ResponseStatus.Error
                    };
                }
            }
        }

    }
}
