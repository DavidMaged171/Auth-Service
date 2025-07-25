using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagerController : Controller
    {
        private readonly IRoleManagerProcessor _roleManagerProcessor;
        private readonly ILogger _logger;
        public RoleManagerController(IRoleManagerProcessor roleManagerProcessor, ILogger logger)
        {
            _roleManagerProcessor = roleManagerProcessor;
            _logger = logger;
        }
        [HttpPost("AssignRoleToUser")]
        [Authorize(Roles ="Admin")]
        public GenericResponseClass<bool> AssignRoleToUser(RoleAssignmentRequestDto requestDto)
        {
            return GenericExceptionHandler.Handle(() => 
            {
                return _roleManagerProcessor.AssignRoleToUser(requestDto).Result;
            },_logger);
        }
    }
}
