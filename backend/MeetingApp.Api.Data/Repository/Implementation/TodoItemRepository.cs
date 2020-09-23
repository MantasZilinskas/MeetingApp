using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(MeetingAppContext context) : base(context) { }

        public async Task<bool> IsDuplicate(TodoItem entity)
        {
            return await _context.TodoItems.AnyAsync(todoItem => todoItem.Name == entity.Name);
        }
    }
}
