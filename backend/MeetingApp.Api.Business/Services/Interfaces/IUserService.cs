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
        public Task<IdentityResult> InsertUser(UserRequest user);
        public Task<IdentityResult> Register(UserRequest user);
        public Task<LoginResponse> Login(string userName, string password);
        public Task<UserResponse> GetUserProfile(string userId);
        public Task<List<UserResponse>> GetAllUsers();
        public Task<IdentityResult> DeleteUser(string userId);
        public Task<IdentityResult> UpdateUser(UserRequest user, string userId);
        public Task<GenericSliceDTO<UserResponse>> GetSlice(SliceRequest request);
    }
}
