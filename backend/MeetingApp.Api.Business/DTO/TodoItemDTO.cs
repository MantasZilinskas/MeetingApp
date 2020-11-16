using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        [Required]
        public int MeetingId { get; set; }
        public string UserId { get; set; }
    }
}
