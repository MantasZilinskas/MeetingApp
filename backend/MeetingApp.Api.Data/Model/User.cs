using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace MeetingApp.Api.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<UserMeeting> UserMeetings { get; set; }
        public IEnumerable<TodoItem> TodoItems { get; set; }
    }
}
