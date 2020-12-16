using System.Collections.Generic;

namespace MeetingApp.Api.Data.Model
{
    public class LoginResponseDao
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
