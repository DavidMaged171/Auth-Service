
using Auth.Applicatoin.DTOs.Resopnse;

namespace Auth.Applicatoin.BusinessInterfaces
{
    public interface IUserManagerProcessor
    {
        public Task<GenericResponseClass<bool>> DeleteUser(int userId);
    }
}
