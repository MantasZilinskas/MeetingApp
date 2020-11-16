using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingApp.Api.Data.Model
{
    public class Meeting
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public List<TodoItem> TodoItems { get; set; }
        public List<User> Users { get; set; }
    }
}
