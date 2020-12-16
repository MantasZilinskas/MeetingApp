using MeetingApp.Api.Data.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<IdentityResult> InsertUser(User user, string password, IList<string> roles);
        public Task<IdentityResult> Register(User user, string password);
        public Task<LoginResponseDao> Login(string userName, string password);
        public Task<User> GetUser(string userId);
        public Task<List<User>> GetAllUsers();
        public Task<IdentityResult> DeleteUser(string userId);
        public Task<IdentityResult> UpdateUser(User user, string userId, string newpass, IList<string> roles);
        public Task<List<User>> GetSlice(SliceRequestDao request);
        public Task<int> GetCount();
        public Task<IList<string>> GetUserRoles(string userId);
        public Task<List<Meeting>> GetUserMeetingSlice(string userId, SliceRequestDao request);
        public Task<int> GetUserMeetingCount(string userId);
    }
}
