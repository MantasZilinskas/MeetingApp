using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingApp.Api.Business.Mapping
{
    public class TodoItemDTOProfile : Profile
    {
        public TodoItemDTOProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>()
                .ReverseMap();
        }
    }
}
