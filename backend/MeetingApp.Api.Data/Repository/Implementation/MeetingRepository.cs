using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MeetingAppContext _context;
        private readonly UserManager<User> _userManager;

        public MeetingRepository(MeetingAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> IsDuplicateName(Meeting resource)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Name == resource.Name);
        }
        public async Task<int> GetCount()
        {
            return await _context.Meetings
                .CountAsync();
        }
        public async Task<Meeting> Get(int id)
        {
            return await _context.Meetings
                .Include(meeting => meeting.TodoItems)
                .Include(meeting => meeting.Users)
                .FirstOrDefaultAsync(meeting => meeting.Id == id);
        }
        public async Task<List<Meeting>> GetSlice(SliceRequestDAO request)
        {
            if (request.order == "asc")
            {
                return request.orderBy switch
                {
                    "name" => await _context.Meetings
                            .OrderBy(i => i.Name)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "description" => await _context.Meetings
                            .OrderBy(i => i.Description)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    _ => null
                };
            }
            else
            {
                return request.orderBy switch
                {
                    "name" => await _context.Meetings
                            .OrderByDescending(i => i.Name)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "description" => await _context.Meetings
                            .OrderByDescending(i => i.Description)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    _ => null
                };
            }
        }
        public async Task<List<Meeting>> GetAll()
        {
            var meetings = await _context.Meetings
                 .ToListAsync();
            return meetings;
        }
        public async Task<Meeting> Delete(int meetingId)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            if(meeting != null)
            {
                _context.Meetings.Remove(meeting);
                await _context.SaveChangesAsync();
            }
            return meeting;
        }

        public async Task<Meeting> Insert(Meeting meeting)
        {
            meeting.TextEditorData = "";
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();
            return meeting;
        }

        public async Task<Meeting> Update(int id, Meeting meeting)
        {
            Meeting existing = await _context.Meetings.AsNoTracking().FirstOrDefaultAsync(value => value.Id == id);
            if (existing != null)
            {
                meeting.Id = id;
                _context.Meetings.Update(meeting);
                await _context.SaveChangesAsync();
            }
            return existing;
        }
        public async Task<string> InsertMeetingUser(string userId, int meetingId)
        {
            var meeting = await _context.Meetings
               .Include(meeting => meeting.Users)
               .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
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
            if (user == null)
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
            if(meeting == null)
            {
                throw new KeyNotFoundException();
            }
            return meeting.Users;
        }
        public async Task<User> GetMeetingUser(int meetingId, string userId)
        {
            var meeting = await _context.Meetings
                .Include(meeting => meeting.Users)
                .FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            if (meeting == null)
            {
                throw new KeyNotFoundException();
            }
            var user = meeting.Users.FirstOrDefault(user => user.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
            return user;
        }
        public async Task<bool> UpdateTextEditorData(int meetingId, string textEditorData)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(meeting => meeting.Id == meetingId);
            if(meeting == null)
            {
                return false;
            }
            meeting.TextEditorData = textEditorData;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> MeetingExists(int meetingId)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Id == meetingId);
        }
    }
}
