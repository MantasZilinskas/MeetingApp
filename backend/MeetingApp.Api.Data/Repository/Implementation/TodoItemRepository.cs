using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly MeetingAppContext _context;
        public TodoItemRepository(MeetingAppContext context)
        {
            _context = context;
        }

        public async Task DeleteMeetingItems(int meetingId)
        {
            var itemsToDelete = await _context.TodoItems.Where(entity => entity.Meeting.Id == meetingId).ToListAsync();
            _context.TodoItems.RemoveRange(itemsToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMeetingItems(List<TodoItem> todoItems)
        {
            foreach (var value in todoItems)
            {
                _context.TodoItems.Add(value);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<TodoItem>> GetMeetingTodoItems(int meetingId)
        {
            return await _context.TodoItems.Where(value => value.Meeting.Id == meetingId).ToListAsync();
        }
        public async Task<TodoItem> GetMeetingTodoItem(int meetingId, int todoItemId)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == todoItemId && value.Meeting.Id == meetingId);
        }

        public async Task<TodoItem> Insert(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> Update(int todoItemId, TodoItem todoItem)
        {
            var existing = await _context.TodoItems
                .FirstOrDefaultAsync(value => value.Id == todoItemId && value.Meeting.Id == todoItem.MeetingId);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(todoItem);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public async Task Delete(TodoItem todoItem)
        {
            _context.Set<TodoItem>().Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Get(int id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(todoItem => todoItem.Id == id);
        }

        public async Task<ICollection<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }
    }
}
