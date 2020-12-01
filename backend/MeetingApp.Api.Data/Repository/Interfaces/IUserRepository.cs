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
        public Task<LoginResponseDAO> Login(string userName, string password);
        public Task<User> GetUserProfile(string userId);
        public Task<List<User>> GetAllUsers();
        public Task<IdentityResult> DeleteUser(string userName);
        public Task<IdentityResult> UpdateUser(User user, string userId);
    }
}
