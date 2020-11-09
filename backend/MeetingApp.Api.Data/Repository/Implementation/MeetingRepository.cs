using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(MeetingAppContext context): base(context){}

        public async Task<bool> IsDuplicateName(Meeting resource)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Name == resource.Name);
        }
        public override async Task<Meeting> Get(int id)
        {
            return await _context.Meetings
                .Include(meeting => meeting.TodoItems)
                .Include(meeting => meeting.MeetingUsers)
                .ThenInclude(user => user.User)
                .FirstOrDefaultAsync(meeting => meeting.Id == id);
        }
        public override async Task<ICollection<Meeting>> GetAll()
        {
            var meetings = await _context.Meetings
                 .ToListAsync();
            return meetings;
        }
    }
}
