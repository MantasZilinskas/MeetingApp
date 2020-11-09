using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IMeetingService
    {
        public Task<MeetingDTO> Insert(MeetingDTO resource);
        public Task<MeetingDTO> Update(int id, MeetingDTO resource);
        public Task<MeetingDTO> Delete(int id);
        public Task<MeetingDTO> Get(int id);
        public Task<ICollection<MeetingDTO>> GetAll();
        public Task<ICollection<TodoItemDTO>> GetMeetingTodoItems(int meetingId);
    }
}
