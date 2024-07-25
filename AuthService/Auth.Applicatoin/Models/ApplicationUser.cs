using Microsoft.AspNetCore.Identity;

namespace Auth.Applicatoin.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
