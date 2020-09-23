using MeetingApp.Api.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        public Task<bool> IsDuplicateName(TodoItem resource);
        public Task InsertMeetingItems(List<TodoItem> todoItems);
        public Task DeleteMeetingItems(int meetingId);
    }
}
