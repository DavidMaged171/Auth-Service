using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagerController : Controller
    {
        private readonly IRoleManagerProcessor _roleManagerProcessor;
        public RoleManagerController(IRoleManagerProcessor roleManagerProcessor)
        {
            _roleManagerProcessor = roleManagerProcessor;
        }
        [HttpPost("AssignRoleToUser")]
        [Authorize(Roles ="Admin")]
        public GenericResponseClass<bool> AssignRoleToUser(RoleAssignmentRequestDto requestDto)
        {
            return GenericExceptionHandler.Handle(() => 
            {
                return _roleManagerProcessor.AssignRoleToUser(requestDto).Result;
            });
        }
    }
}
