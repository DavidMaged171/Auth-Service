using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthProcessor _authProcessor;
        public AuthController(IAuthProcessor authProcessor)
        {
            _authProcessor = authProcessor;
        }

        [HttpPost("Register")]
        public async Task <RegestrationResponse> ResultAsync(RegisterationRequest request)
        {
            return _authProcessor.RegisterNewUser(request).Result;
        }
    }
}
