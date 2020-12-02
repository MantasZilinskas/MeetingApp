using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Model
{
    public class SliceRequestDAO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int page { get; set; }

        [Required]
        [Range(1, 100)]
        public int rowsPerPage { get; set; }

        [Required]
        [MaxLength(4)]
        public string order { get; set; }

        [Required]
        [MaxLength(11)]
        public string orderBy { get; set; }
    }
}
