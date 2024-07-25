using Auth.Applicatoin.Models;

namespace Auth.Applicatoin.DTOs.Resopnse
{
    public class RegestrationResponse
    {
        public ApplicationUser User { get; set; }
        public bool isRegistered { get; set; }
        public string token { get; set; }
        public List<string> errors { get; set; }
    }
}
