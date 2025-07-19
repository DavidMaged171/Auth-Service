using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applicatoin.BusinessLogic
{
    public class UserManagerProcessor:IUserManagerProcessor
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerProcessor(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }
        public async Task<GenericResponseClass<bool>> DeleteUser(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted == false);
            if (user == null)
            {
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
                    return new GenericResponseClass<bool>
                    {
                        Result = true,
                        ResponseMessage = ResponseMessages.UserDeletedSsuccessfully,
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
        }

    }
}
