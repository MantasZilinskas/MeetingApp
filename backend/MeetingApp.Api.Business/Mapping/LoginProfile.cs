using MeetingApp.Api.Business.DTO;
using AutoMapper;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Business.Mapping
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginResponse, LoginResponseDao>()
                .ReverseMap();
        }
    }
}
