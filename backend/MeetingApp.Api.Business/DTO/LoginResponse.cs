using System.Collections.Generic;

namespace MeetingApp.Api.Business.DTO
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
