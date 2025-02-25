using Auth.Applicatoin.DTOs.Requests;
using Auth.Infrastructure.Models;

namespace Auth.Applicatoin.Mappers.FromDTOToEntity
{
    public static class FromUserDTOToUser
    {
        public static ApplicationUser Map(RegisterationRequest registerationRequest)
        {
            return new ApplicationUser() 
            {
                FirstName=registerationRequest.FirstName,
                LastName=registerationRequest.LastName,
                Email=registerationRequest.Email,
                Password=registerationRequest.Password,
            };
        }
    }
}
