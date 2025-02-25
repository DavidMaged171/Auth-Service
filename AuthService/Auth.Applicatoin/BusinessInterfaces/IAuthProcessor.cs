using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;

namespace Auth.Applicatoin.BusinessInterfaces
{
    public interface IAuthProcessor
    {
        public Task<GenericResponseClass<RegestrationResponse>> RegisterNewUser(RegisterationRequest request);
        public Task<GenericResponseClass<LoginResponse>> Login(LoginRequest request);

    }
}
