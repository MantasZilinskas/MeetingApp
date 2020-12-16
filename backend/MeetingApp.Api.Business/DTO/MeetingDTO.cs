using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Api.Business.DTO
{
    public class MeetingDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string TextEditorData { get; set; }
    }
}
