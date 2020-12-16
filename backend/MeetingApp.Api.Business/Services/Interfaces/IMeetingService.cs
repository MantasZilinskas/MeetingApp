using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IMeetingService
    {
        public Task<MeetingDto> Insert(MeetingDto resource);
        public Task<MeetingDto> Update(int id, MeetingDto resource);
        public Task<MeetingDto> Delete(int id);
        public Task<MeetingDto> Get(int id);
        public Task<ICollection<MeetingDto>> GetAll();
        public Task<ICollection<TodoItemDto>> GetMeetingTodoItems(int meetingId);
        public Task<string> InsertMeetingUser(string userId, int meetingId);
        public Task DeleteMeetingUser(int meetingId, string userId);
        public Task<List<UserResponse>> GetAllMeetingUsers(int meetingId);
        public Task<UserRequest> GetMeetingUser(int meetingId, string userId);
        public Task<GenericSliceDto<MeetingDto>> GetSlice(SliceRequest request);
        public Task<bool> UpdateTextEditorData(int meetingId, string textEditorData);
    }
}
