using MeetingApp.Api.Data.Model;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        public Task<bool> IsDuplicate(TodoItem entity);
    }
}
