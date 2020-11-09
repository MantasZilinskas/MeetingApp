﻿using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using MeetingApp.Api.Data.Model;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class UserMeetingRepository : IUserMeetingRepository
    {
        private readonly MeetingAppContext _context;

        public UserMeetingRepository(MeetingAppContext context)
        {
            _context = context;
        }

        public async Task DeleteMeetingUsers(int meetingId)
        {
            var entitiesToRemove = await _context.UserMeetings.Where(entity => entity.MeetingId == meetingId).ToListAsync();
            _context.UserMeetings.RemoveRange(entitiesToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task Insert(List<int> userIds, int meetingId)
        {
            foreach(var userId in userIds)
            {
                _context.Add(new UserMeeting { MeetingId = meetingId, UserId = userId });
            }
            await _context.SaveChangesAsync();
        }
        public async Task Update(List<int> userIds, int meetingId)
        {
            await DeleteMeetingUsers(meetingId);
            await Insert(userIds, meetingId);
        }
        public async Task<ICollection<Meeting>> GetUserMeetings(int userId)
        {
            return await _context.UserMeetings
                .Where(value => value.UserId == userId)
                .Select(value => value.Meeting)
                .ToListAsync();
        }
        public async Task<ICollection<User>> GetMeetingUsers(int meetingId)
        {
            return await _context.UserMeetings
                .Where(value => value.MeetingId == meetingId)
                .Select(value => value.User)
                .ToListAsync();
        }
    }
}
