using AutoMapper;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MeetingApp.Api.Business.Mapping
{
    public class MeetingDTOProfile: Profile
    {
        public MeetingDTOProfile()
        {
            CreateMap<Meeting, MeetingDTO>()
                .ReverseMap();
        }
    }
}
