//using AutoMapper;
//using MeetingApp.Api.Business.DTO;
//using MeetingApp.Api.Business.Services.Interfaces;
//using MeetingApp.Api.Data.Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace MeetingApp.Api.Business.Services.Implementation
//{
//    public class UserMeetingService : IUserMeetingService
//    {
//        private readonly IUserMeetingRepository _userMeetingRepo;
//        private readonly IMeetingRepository _meetingRepository;
//        private readonly IMapper _mapper;

//        public UserMeetingService(IUserMeetingRepository userMeetingRepo, IMeetingRepository meetingRepository, IMapper mapper)
//        {
//            _userMeetingRepo = userMeetingRepo;
//            _meetingRepository = meetingRepository;
//            _mapper = mapper;
//        }
//        public async Task InsertMeetingUsers(int meetingId, List<UserDTO> users)
//        {
//            var meeting = await _meetingRepository.Get(meetingId);
//            if (meeting == null)
//            {
//                throw new KeyNotFoundException();
//            }
//            await _userMeetingRepo.Insert(users.Select(user => user.Id).ToList(), meetingId);
//        }
//        public async Task<ICollection<UserDTO>> GetMeetingUsers(int meetingId)
//        {
//            var meeting = await _meetingRepository.Get(meetingId);
//            if (meeting == null)
//            {
//                throw new KeyNotFoundException();
//            }
//            return _mapper.Map<ICollection<UserDTO>>(await _userMeetingRepo.GetMeetingUsers(meetingId));
//        }
//        public async Task UpdateMeetingUsers(int meetingId, List<UserDTO> users)
//        {
//            var meeting = await _meetingRepository.Get(meetingId);
//            if (meeting == null)
//            {
//                throw new KeyNotFoundException();
//            }
//            await _userMeetingRepo.Update(users.Select(user => user.Id).ToList(), meetingId);
//        }
//        public async Task DeleteMeetingUsers(int meetingId)
//        {
//            var meeting = await _meetingRepository.Get(meetingId);
//            if (meeting == null)
//            {
//                throw new KeyNotFoundException();
//            }
//            await _userMeetingRepo.DeleteMeetingUsers(meetingId);
//        }
//    }
//}
