using MeetingApp.Api.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface ITodoItemRepository
    {
        public Task InsertMeetingItems(List<TodoItem> todoItems);
        public Task DeleteMeetingItems(int meetingId);
        public Task<ICollection<TodoItem>> GetMeetingTodoItems(int meetingId);
        public Task<TodoItem> GetMeetingTodoItem(int meetingId, int todoItemId);
        public Task<TodoItem> Insert(TodoItem resource);
        public Task<TodoItem> Update(int id, TodoItem resource);
        public Task Delete(TodoItem resource);
        public Task<TodoItem> Get(int id);
        public Task<ICollection<TodoItem>> GetAll();
    }
}
