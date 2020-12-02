using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }
        public async Task<UserResponse> GetUserProfile(string userId)
        {
            var user = await _userRepo.GetUserProfile(userId);
            return _mapper.Map<UserResponse>(user);
        }
        public async Task<IdentityResult> InsertUser(UserRequest user)
        {
            var userEntity = _mapper.Map<User>(user);
            var result = await _userRepo.InsertUser(userEntity, user.Password, user.Roles);
            return result;
        }
        public async Task<LoginResponse> Login(string userName, string password)
        {
            var result = await _userRepo.Login(userName, password);
            return _mapper.Map<LoginResponse>(result);
        }
        public async Task<GenericSliceDTO<UserResponse>> GetSlice(SliceRequest request)
        {
            var users = await _userRepo.GetSlice(_mapper.Map<SliceRequestDAO>(request));
            var count = await _userRepo.GetCount();
            if (users == null) return null;
            return new GenericSliceDTO<UserResponse> { Items = _mapper.Map<List<UserResponse>>(users), TotalCount = count };
        }
        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            return _mapper.Map<List<UserResponse>>(users);
        }
        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var result = await _userRepo.DeleteUser(userName);
            return result;
        }
        public async Task<IdentityResult> UpdateUser(UserRequest user, string userId)
        {
            var result = await _userRepo.UpdateUser(_mapper.Map<User>(user), userId);
            return result;
        }


    }
}
