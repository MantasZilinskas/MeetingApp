using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepo;
        private readonly IMapper _mapper;

        public TemplateService(ITemplateRepository templateRepo, IMapper mapper)
        {
            _templateRepo = templateRepo;
            _mapper = mapper;
        }

        public async Task<TemplateDTO> Delete(int id)
        {
            var template = await _templateRepo.Get(id);
            if (template != null)
            {
                await _templateRepo.Delete(id);
            }
            return _mapper.Map<TemplateDTO>(template);
        }

        public async Task<TemplateDTO> Get(int id)
        {
            return _mapper.Map<TemplateDTO>(await _templateRepo.Get(id));
        }

        public async Task<ICollection<TemplateDTO>> GetAll()
        {
            return _mapper.Map<ICollection<TemplateDTO>>(await _templateRepo.GetAll());
        }

        public async Task<TemplateDTO> Insert(TemplateDTO dto)
        {
            var template = _mapper.Map<Template>(dto);
            var returnedEntity = await _templateRepo.Insert(template);
            return _mapper.Map<TemplateDTO>(returnedEntity);
        }

        public async Task<TemplateDTO> Update(int templateId, TemplateDTO dto)
        {
            return _mapper.Map<TemplateDTO>(await _templateRepo.Update(templateId, _mapper.Map<Template>(dto)));
        }
    }
}
