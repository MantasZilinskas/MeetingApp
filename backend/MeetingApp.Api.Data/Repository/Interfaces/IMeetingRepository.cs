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
        public Task<Meeting> Delete(int meetingId);
        public Task<Meeting> Get(int id);
        public Task<List<Meeting>> GetAll();
        public Task<string> InsertMeetingUser(string userId, int meetingId);
        public Task DeleteMeetingUser(int meetingId, string userId);
        public Task<List<User>> GetAllMeetingUsers(int meetingId);
        public Task<User> GetMeetingUser(int meetingId, string userId);
        public Task<bool> MeetingExists(int meetingId);
        public Task<List<Meeting>> GetSlice(SliceRequestDAO request);
        public Task<int> GetCount();
    }
}
