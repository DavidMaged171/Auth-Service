
using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;

namespace Auth.Applicatoin.BusinessInterfaces
{
    public interface IAuthProcessor
    {
        Task<RegestrationResponse> RegisterNewUser(RegisterationRequest request);
    }
}
