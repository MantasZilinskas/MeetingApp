using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IUserMeetingRepository
    {
        public Task Insert(List<int> userIds, int meetingId);
        public Task Update(List<int> userIds, int meetingId);
        public Task Delete(int meetingId);
    }
}
