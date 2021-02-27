using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface ITemplateRepository
    {
        public Task<bool> IsDuplicateName(Template resource);
        public Task<Template> Insert(Template resource);
        public Task<Template> Update(int id, Template resource);
        public Task<Template> Delete(int meetingId);
        public Task<Template> Get(int id);
        public Task<List<Template>> GetAll();
        public Task<bool> TemplateExists(int meetingId);
        public Task<List<Template>> GetSlice(SliceRequestDao request);
        public Task<int> GetCount();
    }
}
