using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace MeetingApp.Api.Data.Model
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<TodoItem> TodoItems { get; set; }
    }
}
