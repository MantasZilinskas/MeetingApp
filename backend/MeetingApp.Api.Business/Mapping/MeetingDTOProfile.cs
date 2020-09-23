using AutoMapper;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;

namespace MeetingApp.Api.Business.Mapping
{
    public class MeetingDTOProfile: Profile
    {
        public MeetingDTOProfile()
        {
            CreateMap<Meeting, MeetingDTO>()
               .ForMember(x => x.Users, opt => opt.Ignore())
               .ForMember(x => x.TodoItems, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.MeetingUsers, opt => opt.Ignore())
               .ForMember(x => x.TodoItems, opt => opt.Ignore());
        }
    }
}
