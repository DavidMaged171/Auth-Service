using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Auth.Applicatoin.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthProcessor _authProcessor;
        private readonly ILogger _logger;
        public AuthController(IAuthProcessor authProcessor, ILogger logger)
        {
            _authProcessor = authProcessor;
            _logger = logger;
        }

        [HttpPost("Register")]
        public GenericResponseClass<RegestrationResponse> Register(RegisterationRequest request)
        {
            return GenericExceptionHandler.Handle(() =>
            {
                return _authProcessor.RegisterNewUser(request).Result;
            },_logger);
        }
        [HttpPost("Login")]
        public GenericResponseClass<LoginResponse> Login(LoginRequest request)
        {
            return GenericExceptionHandler.Handle(() =>
            {
                return _authProcessor.Login(request).Result;
            },_logger);
        }
        
    }
}
