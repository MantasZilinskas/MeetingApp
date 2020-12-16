using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Business.Mapping
{
    public class SliceRequestProfile: Profile
    {
        public SliceRequestProfile()
        {
            CreateMap<SliceRequest, SliceRequestDao>()
                .ReverseMap();
        }
    }
}
