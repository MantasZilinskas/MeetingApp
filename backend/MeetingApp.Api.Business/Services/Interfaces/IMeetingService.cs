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
        public Task<string> InsertMeetingUser(string userId, int meetingId);
        public Task DeleteMeetingUser(int meetingId, string userId);
        public Task<List<UserResponse>> GetAllMeetingUsers(int meetingId);
        public Task<UserRequest> GetMeetingUser(int meetingId, string userId);
        public Task<GenericSliceDTO<MeetingDTO>> GetSlice(SliceRequest request);
    }
}
