
using Auth.Applicatoin.Enums;

namespace Auth.Applicatoin.DTOs.Resopnse
{
    public class GenericResponseClass <T>
    {
        public T Result { get; set; }
        public ResponseStatus Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
