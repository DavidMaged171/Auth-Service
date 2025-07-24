
using Microsoft.Win32;

namespace Auth.Applicatoin.DTOs.Resopnse
{
    public static class ResponseMessages
    {
        public static string EmailAlreadyRegistered { get { return "This Email already registered before"; } }
        public static string UserNameAlreadyRegistered { get { return "This user name already registered before"; } }
        public static string UserRegisteredSuccessfully { get { return "User Registered Successfully"; } }
        public static string ErrorsWhileRegisterUser { get { return "Error while reister user"; } }
        public static string LoggedInSuccessfully { get { return "Logged in successfully"; } }
        public static string UserNotRegistered { get { return "User Not Registered"; } }
        public static string ErrorsWhileLogin { get { return "User name or password invalid"; } }
        public static string UserNotFound { get { return "User not found"; } }
        public static string UserDeletedSsuccessfully{ get { return "User deleted successfully"; } }
        public static string GenericError{ get { return "Error occoured, Contact system admin"; } }
        public static string RoleNotFound{ get { return "Can't find role with this name"; } }
        public static string UserAlreadyInRole{ get { return "User already in role"; } }
        public static string UserAssignedToRoleSuccessfully{ get { return "User assigned to role successfully"; } }
    }
}
