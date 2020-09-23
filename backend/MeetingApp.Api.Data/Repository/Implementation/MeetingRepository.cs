using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(MeetingAppContext context): base(context){}

        public async Task<bool> IsDuplicate(Meeting entity)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Name == entity.Name);
        }
    }
}
