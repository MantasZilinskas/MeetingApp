using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepo;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository, IMapper mapper)
        {
            _todoItemRepo = todoItemRepository;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> Delete(int id)
        {
            var todoItem = await _todoItemRepo.Get(id);
            if (todoItem != null)
            {
                await _todoItemRepo.Delete(todoItem);
            }
            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task<TodoItemDTO> Get(int id)
        {
            return _mapper.Map<TodoItemDTO>(await _todoItemRepo.Get(id));
        }

        public async Task<ICollection<TodoItemDTO>> GetAll()
        {
            return _mapper.Map<ICollection<TodoItemDTO>>(await _todoItemRepo.GetAll());
        }

        public async Task<TodoItemDTO> Insert(TodoItemDTO dto)
        {
            var todoItem = _mapper.Map<TodoItem>(dto);
            if (await _todoItemRepo.IsDuplicate(todoItem))
            {
                return null;
            }
            var returnedEntity = await _todoItemRepo.Insert(todoItem);
            return _mapper.Map<TodoItemDTO>(returnedEntity);
        }

        public async Task<TodoItemDTO> Update(int id, TodoItemDTO dto)
        {
            var todoItemDto = _mapper.Map<TodoItemDTO>(await _todoItemRepo.Get(id));
            var todoItemEntity = _mapper.Map<TodoItem>(dto);
            if (await _todoItemRepo.IsDuplicate(todoItemEntity))
            {
                return null;
            }
            if (todoItemDto != null)
            {
                return _mapper.Map<TodoItemDTO>(await _todoItemRepo.Update(id, todoItemEntity));
            }
            return todoItemDto;
        }
    }
}
