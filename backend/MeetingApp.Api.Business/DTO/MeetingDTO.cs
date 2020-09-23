using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.DTO
{
    public class MeetingDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TodoItemDTO> TodoItems { get; set; }
        public List<int> UserIds { get; set; }
    }
}
