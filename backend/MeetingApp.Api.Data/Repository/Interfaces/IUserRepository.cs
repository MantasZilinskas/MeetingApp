using MeetingApp.Api.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<IdentityResult> InsertUser(User user, string password, IList<string> roles);
        public Task<IdentityResult> Register(User user, string password);
        public Task<LoginResponseDAO> Login(string userName, string password);
        public Task<User> GetUser(string userId);
        public Task<List<User>> GetAllUsers();
        public Task<IdentityResult> DeleteUser(string userId);
        public Task<IdentityResult> UpdateUser(User user, string userId, string newpass, IList<string> roles);
        public Task<List<User>> GetSlice(SliceRequestDAO request);
        public Task<int> GetCount();
        public Task<IList<string>> GetUserRoles(string userId);
        public Task<List<Meeting>> GetUserMeetingSlice(string userId, SliceRequestDAO request);
        public Task<int> GetUserMeetingCount(string userId);
    }
}
