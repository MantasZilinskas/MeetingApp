﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.DTO
{
    public class MeetingSliceDTO
    {
        public List<MeetingDTO> Meetings{ get; set; }
        public int TotalCount { get; set; }
    }
}
