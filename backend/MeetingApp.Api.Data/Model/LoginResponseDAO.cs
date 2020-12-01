using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Model
{
    public class LoginResponseDAO
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
