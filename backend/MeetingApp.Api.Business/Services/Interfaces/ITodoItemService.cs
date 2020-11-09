using MeetingApp.Api.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface ITodoItemService
    {
        public Task<TodoItemDTO> Insert(TodoItemDTO resource);
        public Task<TodoItemDTO> Update(int id, TodoItemDTO resource);
        public Task<TodoItemDTO> Delete(int id, int meetingId);
        public Task<TodoItemDTO> Get(int id, int meetingId);
        public Task<ICollection<TodoItemDTO>> GetAll();
    }
}
