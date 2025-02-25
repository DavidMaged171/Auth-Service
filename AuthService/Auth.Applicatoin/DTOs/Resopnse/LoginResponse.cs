
namespace Auth.Applicatoin.DTOs.Resopnse
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
