using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface ITemplateService
    {
        public Task<TemplateDTO> Insert(TemplateDTO resource);
        public Task<TemplateDTO> Update(int id, TemplateDTO resource);
        public Task<TemplateDTO> Delete(int id);
        public Task<TemplateDTO> Get(int id);
        public Task<ICollection<TemplateDTO>> GetAll();
        public Task<GenericSliceDto<TemplateDTO>> GetSlice(SliceRequest request);
    }
}
