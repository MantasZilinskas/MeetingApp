using MeetingApp.Api.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IUserMeetingService
    {
        public Task InsertMeetingUsers(int meetingId, List<UserDTO> users);
        public Task<ICollection<UserDTO>> GetMeetingUsers(int meetingId);
        public Task UpdateMeetingUsers(int meetingId, List<UserDTO> users);
        public Task DeleteMeetingUsers(int meetingId);
    }
}
