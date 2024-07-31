
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    public class AuthController : Controller
    {

        public AuthController()
        {

        }

        [HttpPost("Register")]
        public async Task <RegestrationResponse> ResultAsync(RegisterationRequest request)
        {
            return null;
        }
    }
}
