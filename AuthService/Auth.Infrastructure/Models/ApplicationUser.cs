using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser():base(){} 
        public override string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
