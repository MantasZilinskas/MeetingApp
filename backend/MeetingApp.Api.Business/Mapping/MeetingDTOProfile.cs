using AutoMapper;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Business.DTO;

namespace MeetingApp.Api.Business.Mapping
{
    public class MeetingDtoProfile: Profile
    {
        public MeetingDtoProfile()
        {
            CreateMap<Meeting, MeetingDto>()
                .ReverseMap();
        }
    }
}
