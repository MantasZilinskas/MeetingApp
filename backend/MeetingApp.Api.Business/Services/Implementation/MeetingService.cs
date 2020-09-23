using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _repo;
        private readonly IMapper _mapper;

        public MeetingService(IMeetingRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        public async Task<MeetingDTO> Delete(int id)
        {
            var meeting = await _repo.Get(id);
            if(meeting != null)
            {
                await _repo.Delete(meeting);
            }
            return _mapper.Map<MeetingDTO>(meeting);
        }

        public async Task<MeetingDTO> Get(int id)
        {
            return _mapper.Map<MeetingDTO>(await _repo.Get(id));
        }

        public async Task<ICollection<MeetingDTO>> GetAll()
        {
            return _mapper.Map<ICollection<MeetingDTO>>(await _repo.GetAll());
        }

        public async Task<MeetingDTO> Insert(MeetingDTO dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            if(await _repo.IsDuplicate(meeting))
            {
                return null;
            }
            var returnedEntity = await _repo.Insert(meeting);
            return _mapper.Map<MeetingDTO>(returnedEntity);
        }

        public async Task<MeetingDTO> Update(int id, MeetingDTO dto)
        {
            var meetingDto = _mapper.Map<MeetingDTO>(await _repo.Get(id));
            var meetingEntity = _mapper.Map<Meeting>(dto);
            if (await _repo.IsDuplicate(meetingEntity))
            {
                return null;
            }
            if (meetingDto != null)
            {
                return _mapper.Map<MeetingDTO>(await _repo.Update(id, meetingEntity));
            }
            return meetingDto;
        }
    }
}
