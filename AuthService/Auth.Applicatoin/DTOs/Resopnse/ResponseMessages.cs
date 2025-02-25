
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
    }
}
