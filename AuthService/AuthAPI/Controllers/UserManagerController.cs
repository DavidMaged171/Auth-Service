using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : Controller
    {
        private readonly IUserManagerProcessor _userManagerProcessor;
        private readonly ILogger _logger;
        public UserManagerController(IUserManagerProcessor userManagerProcessor)
        {
            _userManagerProcessor = userManagerProcessor;
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles ="Admin")]
        public GenericResponseClass<bool> DeleteUser(int userId)
        {
            return GenericExceptionHandler.Handle(() =>
            {
                return _userManagerProcessor.DeleteUser(userId).Result;
            }, _logger);
        }
    }
}
