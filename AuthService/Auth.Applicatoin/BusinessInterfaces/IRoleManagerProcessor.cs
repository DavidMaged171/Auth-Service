using Auth.Applicatoin.DTOs.Requests;
using Auth.Applicatoin.DTOs.Resopnse;

namespace Auth.Applicatoin.BusinessInterfaces
{
    public interface IRoleManagerProcessor
    {
        public Task<GenericResponseClass<bool>> AssignRoleToUser(RoleAssignmentRequestDto requestDto);
    }
}
