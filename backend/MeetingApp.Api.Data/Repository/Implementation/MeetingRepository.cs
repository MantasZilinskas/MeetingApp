using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MeetingAppContext _context;
        public MeetingRepository(MeetingAppContext context) {
            _context = context;
        }
        public async Task<bool> IsDuplicateName(Meeting resource)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Name == resource.Name);
        }
        public async Task<Meeting> Get(int id)
        {
            return await _context.Meetings
                .Include(meeting => meeting.TodoItems)
                .Include(meeting => meeting.Users)
                .FirstOrDefaultAsync(meeting => meeting.Id == id);
        }
        public async Task<ICollection<Meeting>> GetAll()
        {
            var meetings = await _context.Meetings
                 .ToListAsync();
            return meetings;
        }
        public async Task Delete(Meeting meeting)
        {
            _context.Set<Meeting>().Remove(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task<Meeting> Insert(Meeting meeting)
        {
            _context.Set<Meeting>().Add(meeting);
            await _context.SaveChangesAsync();
            return meeting;
        }

        public async Task<Meeting> Update(int id, Meeting meeting)
        {
            Meeting existing = await _context.Set<Meeting>().FindAsync(id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(meeting);
                await _context.SaveChangesAsync();
            }
            return existing;
        }
        public async Task<string> InsertMeetingUser(User user, int meetingId)
        {
            var meeting = await _context.Meetings
               .Include(meeting => meeting.Users)
               .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            meeting.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        public async Task DeleteMeetingUser(int meetingId, string userId)
        {
            var meeting = await _context.Meetings
               .Include(meeting => meeting.Users)
               .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            var user = meeting.Users.FirstOrDefault(user => user.Id == userId);
            if(user == null)
            {
                throw new KeyNotFoundException();
            }
            meeting.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetAllMeetingUsers(int meetingId)
        {
            var meeting = await _context.Meetings
                .Include(meeting => meeting.Users)
                .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            return meeting.Users;
        }
        public async Task<User> GetMeetingUser(int meetingId, string userId)
        {
            var meeting = await _context.Meetings
                .Include(meeting => meeting.Users)
                .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            return meeting.Users.FirstOrDefault(user => user.Id == userId);
        }
        public async Task<bool> MeetingExists(int meetingId)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Id == meetingId);
        }
    }
}
