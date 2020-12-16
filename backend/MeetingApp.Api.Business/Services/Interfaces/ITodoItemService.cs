using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface ITodoItemService
    {
        public Task<TodoItemDto> Insert(TodoItemDto resource);
        public Task<TodoItemDto> Update(int id, TodoItemDto resource);
        public Task<TodoItemDto> Delete(int id, int meetingId);
        public Task<TodoItemDto> Get(int id, int meetingId);
        public Task<ICollection<TodoItemDto>> GetAll();
    }
}
