using MeetingApp.Api.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IUserMeetingService
    {
        public Task InsertMeetingUsers(int meetingId, List<UserRequest> users);
        public Task<ICollection<UserRequest>> GetMeetingUsers(int meetingId);
        public Task UpdateMeetingUsers(int meetingId, List<UserRequest> users);
        public Task DeleteMeetingUsers(int meetingId);
    }
}
