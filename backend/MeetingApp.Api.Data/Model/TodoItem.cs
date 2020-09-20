using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingApp.Api.Data.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
