using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingApp.Api.Business.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>()
                .ReverseMap();
            CreateMap<User, UserResponse>()
                .ReverseMap();
        }
    }
}
