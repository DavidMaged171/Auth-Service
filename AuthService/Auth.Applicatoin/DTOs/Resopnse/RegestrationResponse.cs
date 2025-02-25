using Auth.Infrastructure.Models;

namespace Auth.Applicatoin.DTOs.Resopnse
{
    public class RegestrationResponse
    {
        public bool isRegistered { get; set; }
        public List<string> errors { get; set; }
    }
}
