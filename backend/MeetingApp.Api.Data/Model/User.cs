using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MeetingApp.Api.Data.Model
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public List<Meeting> Meetings { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}
