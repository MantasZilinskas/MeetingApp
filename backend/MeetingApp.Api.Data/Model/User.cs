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
        public List<Meeting> Meetings { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}
