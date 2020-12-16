using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Api.Business.DTO
{
    public class TodoItemDto
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
