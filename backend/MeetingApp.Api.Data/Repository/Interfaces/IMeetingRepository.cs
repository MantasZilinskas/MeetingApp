using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IMeetingRepository
    {
        public Task<bool> IsDuplicateName(Meeting resource);
        public Task<Meeting> Insert(Meeting resource);
        public Task<Meeting> Update(int id, Meeting resource);
        public Task Delete(Meeting resource);
        public Task<Meeting> Get(int id);
        public Task<ICollection<Meeting>> GetAll();
        public Task<string> InsertMeetingUser(User user, int meetingId);
        public Task DeleteMeetingUser(int meetingId, string userId);
        public Task<List<User>> GetAllMeetingUsers(int meetingId);
        public Task<User> GetMeetingUser(int meetingId, string userId);
        public Task<bool> MeetingExists(int meetingId);
    }
}
