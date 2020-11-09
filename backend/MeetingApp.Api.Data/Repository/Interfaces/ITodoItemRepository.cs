using MeetingApp.Api.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        public Task InsertMeetingItems(List<TodoItem> todoItems);
        public Task DeleteMeetingItems(int meetingId);
        public Task<ICollection<TodoItem>> GetMeetingTodoItems(int meetingId);
    }
}
