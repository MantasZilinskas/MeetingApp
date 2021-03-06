﻿using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Api.Business.DTO
{
    public class SliceRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Page { get; set; }

        [Required]
        [Range(1, 100)]
        public int RowsPerPage { get; set; }

        [Required]
        [MaxLength(4)]
        public string Order { get; set; }

        [Required]
        [MaxLength(11)]
        public string OrderBy { get; set; }
    }
}
