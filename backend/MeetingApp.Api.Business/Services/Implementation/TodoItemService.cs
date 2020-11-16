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
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository, IMapper mapper, IMeetingRepository meetingRepository)
        {
            _todoItemRepo = todoItemRepository;
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> Delete(int id, int meetingId)
        {
            var todoItem = await _todoItemRepo.GetMeetingTodoItem(meetingId, id);
            if (todoItem != null)
            {
                await _todoItemRepo.Delete(todoItem);
            }
            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task<TodoItemDTO> Get(int id, int meetingId)
        {
            return _mapper.Map<TodoItemDTO>(await _todoItemRepo.GetMeetingTodoItem(meetingId, id));
        }

        public async Task<ICollection<TodoItemDTO>> GetAll()
        {
            return _mapper.Map<ICollection<TodoItemDTO>>(await _todoItemRepo.GetAll());
        }

        public async Task<TodoItemDTO> Insert(TodoItemDTO dto)
        {
            var meeting = await _meetingRepository.Get(dto.MeetingId);
            if (meeting == null)
            {
                return null;
            }
            var todoItem = _mapper.Map<TodoItem>(dto);
            var returnedEntity = await _todoItemRepo.Insert(todoItem);
            return _mapper.Map<TodoItemDTO>(returnedEntity);
        }

        public async Task<TodoItemDTO> Update(int todoItemId, TodoItemDTO dto)
        {
            return _mapper.Map<TodoItemDTO>(await _todoItemRepo.Update(todoItemId, _mapper.Map<TodoItem>(dto)));
        }
    }
}
