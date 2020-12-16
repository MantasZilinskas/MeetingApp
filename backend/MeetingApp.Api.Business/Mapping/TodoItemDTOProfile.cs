using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Business.Mapping
{
    public class TodoItemDtoProfile : Profile
    {
        public TodoItemDtoProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ReverseMap();
        }
    }
}
