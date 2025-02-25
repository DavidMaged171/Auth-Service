using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser():base(){} 
        public int Id { get; set; }
        public override string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
