using MeetingApp.Api.Business.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IdentityResult> InsertUser(UserDTO user);
        public Task<String> Login(string userName, string password);
        public Task<UserDTO> GetUserProfile(string userId);
        public Task<List<UserDTO>> GetAllUsers();
        public Task<IdentityResult> DeleteUser(string userName);
    }
}
