using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepo;
        private readonly IUserMeetingRepository _userMeetingRepo;
        private readonly ITodoItemRepository _todoItemRepo;
        private readonly IMapper _mapper;

        public MeetingService(
            IMeetingRepository meetingRepository,
            IUserMeetingRepository userMeetingRepository,
            ITodoItemRepository todoItemRepository,
            IMapper mapper)
        {
            _meetingRepo = meetingRepository;
            _mapper = mapper;
            _userMeetingRepo = userMeetingRepository;
            _todoItemRepo = todoItemRepository;
        }

        public async Task<MeetingDTO> Delete(int id)
        {
            var meeting = await _meetingRepo.Get(id);
            if(meeting != null)
            {
                await _userMeetingRepo.DeleteMeetingUsers(meeting.Id);
                await _todoItemRepo.DeleteMeetingItems(meeting.Id);
                await _meetingRepo.Delete(meeting);
            }
            return _mapper.Map<MeetingDTO>(meeting);
        }

        public async Task<MeetingDTO> Get(int meetingId)
        {
            var meeting = _mapper.Map<MeetingDTO>(await _meetingRepo.Get(meetingId));
            return meeting;
        }

        public async Task<ICollection<MeetingDTO>> GetAll()
        {
            return _mapper.Map<ICollection<MeetingDTO>>(await _meetingRepo.GetAll());
        }

        public async Task<MeetingDTO> Insert(MeetingDTO dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            if(await _meetingRepo.IsDuplicateName(meeting))
            {
                return null;
            }
            var returnedEntity = await _meetingRepo.Insert(meeting);
            return _mapper.Map<MeetingDTO>(returnedEntity);
        }

        public async Task<MeetingDTO> Update(int id, MeetingDTO dto)
        {
            var meetingDto = _mapper.Map<MeetingDTO>(await _meetingRepo.Get(id));
            var meetingEntity = _mapper.Map<Meeting>(dto);
            if (await _meetingRepo.IsDuplicateName(meetingEntity))
            {
                return null;
            }
            if (meetingDto != null)
            {
                return _mapper.Map<MeetingDTO>(await _meetingRepo.Update(id, meetingEntity));
            }
            return meetingDto;
        }
        public async Task<ICollection<TodoItemDTO>> GetMeetingTodoItems(int meetingId)
        {
            var meeting = await _meetingRepo.Get(meetingId);
            if(meeting == null)
            {
                return null;
            }
            return _mapper.Map<ICollection<TodoItemDTO>>(await _todoItemRepo.GetMeetingTodoItems(meetingId));
        }
    }
}
