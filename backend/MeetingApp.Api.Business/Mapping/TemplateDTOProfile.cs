using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Business.Mapping
{
    public class TemplateDTOProfile: Profile
    {
        public TemplateDTOProfile()
        {
            CreateMap<Template, TemplateDTO>()
                .ReverseMap();
        }
    }
}
