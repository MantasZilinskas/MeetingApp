using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
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

        public async Task<UserDTO> Delete(int id)
        {
            var user = await _userRepo.Get(id);
            if (user != null)
            {
                await _userRepo.Delete(user);
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Get(int id)
        {
            return _mapper.Map<UserDTO>(await _userRepo.Get(id));
        }

        public async Task<ICollection<UserDTO>> GetAll()
        {
            return _mapper.Map<ICollection<UserDTO>>(await _userRepo.GetAll());
        }

        public async Task<UserDTO> Insert(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            if (await _userRepo.IsDuplicateUsername(user))
            {
                return null;
            }
            var returnedEntity = await _userRepo.Insert(user);
            return _mapper.Map<UserDTO>(returnedEntity);
        }

        public async Task<UserDTO> Update(int id, UserDTO dto)
        {
            var userDto = _mapper.Map<UserDTO>(await _userRepo.Get(id));
            var userEntity = _mapper.Map<User>(dto);
            if (await _userRepo.IsDuplicateUsername(userEntity))
            {
                return null;
            }
            if (userDto != null)
            {
                return _mapper.Map<UserDTO>(await _userRepo.Update(id, userEntity));
            }
            return userDto;
        }
    }
}
