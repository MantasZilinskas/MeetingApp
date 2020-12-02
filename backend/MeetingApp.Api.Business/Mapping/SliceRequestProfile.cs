using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Mapping
{
    public class SliceRequestProfile: Profile
    {
        public SliceRequestProfile()
        {
            CreateMap<SliceRequest, SliceRequestDAO>()
                .ReverseMap();
        }
    }
}
