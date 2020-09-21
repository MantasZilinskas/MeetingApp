using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingApp.Api.Data.Model
{
    public class Meeting : BaseEntity
    { 
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public IEnumerable<TodoItem> TodoItems { get; set; }
        public IEnumerable<UserMeeting> MeetingUsers { get; set; }
    }
}
