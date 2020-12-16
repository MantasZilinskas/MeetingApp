using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepo;
        private readonly ITodoItemRepository _todoItemRepo;
        private readonly IMapper _mapper;

        public MeetingService(
            IMeetingRepository meetingRepository,
            ITodoItemRepository todoItemRepository,
            IMapper mapper)
        {
            _meetingRepo = meetingRepository;
            _mapper = mapper;
            _todoItemRepo = todoItemRepository;
        }

        public async Task<MeetingDto> Delete(int meetingId)
        {
            var meeting = await _meetingRepo.Delete(meetingId);
            if(meeting != null)
            {
                await _todoItemRepo.DeleteMeetingItems(meetingId);
            }
            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<MeetingDto> Get(int meetingId)
        {
            var meeting = _mapper.Map<MeetingDto>(await _meetingRepo.Get(meetingId));
            return meeting;
        }
        public async Task<GenericSliceDto<MeetingDto>> GetSlice(SliceRequest request)
        {
            var meetings = await _meetingRepo.GetSlice(_mapper.Map<SliceRequestDao>(request));
            var count = await _meetingRepo.GetCount();
            if (meetings == null) return null;
            return new GenericSliceDto<MeetingDto> { Items = _mapper.Map<List<MeetingDto>>(meetings), TotalCount = count };
        }
        public async Task<ICollection<MeetingDto>> GetAll()
        {
            return _mapper.Map<ICollection<MeetingDto>>(await _meetingRepo.GetAll());
        }

        public async Task<MeetingDto> Insert(MeetingDto dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            if (await _meetingRepo.IsDuplicateName(meeting))
            {
                return null;
            }
            var returnedEntity = await _meetingRepo.Insert(meeting);
            return _mapper.Map<MeetingDto>(returnedEntity);
        }

        public async Task<MeetingDto> Update(int id, MeetingDto dto)
        {
            var meetingEntity = _mapper.Map<Meeting>(dto);
            if (await _meetingRepo.IsDuplicateName(meetingEntity))
            {
                return null;
            }
            return _mapper.Map<MeetingDto>(await _meetingRepo.Update(id, meetingEntity));
        }
        public async Task<ICollection<TodoItemDto>> GetMeetingTodoItems(int meetingId)
        {
            var meeting = await _meetingRepo.Get(meetingId);
            if (meeting == null)
            {
                return null;
            }
            return _mapper.Map<ICollection<TodoItemDto>>(await _todoItemRepo.GetMeetingTodoItems(meetingId));
        }
        public async Task<string> InsertMeetingUser(string userId, int meetingId)
        {
            if (!await _meetingRepo.MeetingExists(meetingId))
            {
                throw new KeyNotFoundException();
            }
            var returnedUserId = await _meetingRepo.InsertMeetingUser(userId, meetingId);
            return returnedUserId;
        }
        public async Task DeleteMeetingUser(int meetingId, string userId)
        {
            if (!await _meetingRepo.MeetingExists(meetingId))
            {
                throw new KeyNotFoundException();
            }

            await _meetingRepo.DeleteMeetingUser(meetingId, userId);
        }
        public async Task<List<UserResponse>> GetAllMeetingUsers(int meetingId)
        {
            if (!await _meetingRepo.MeetingExists(meetingId))
            {
                throw new KeyNotFoundException();
            }
            return _mapper.Map<List<UserResponse>>(await _meetingRepo.GetAllMeetingUsers(meetingId));
        }
        public async Task<UserRequest> GetMeetingUser(int meetingId, string userId)
        {
            if (!await _meetingRepo.MeetingExists(meetingId))
            {
                throw new KeyNotFoundException();
            }
            return _mapper.Map<UserRequest>(await _meetingRepo.GetMeetingUser(meetingId, userId));
        }
        public async Task<bool> UpdateTextEditorData(int meetingId, string textEditorData)
        {
            return await _meetingRepo.UpdateTextEditorData(meetingId, textEditorData);
        }
    }
}
