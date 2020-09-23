using MeetingApp.Api.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> Insert(UserDTO resource);
        public Task<UserDTO> Update(int id, UserDTO resource);
        public Task<UserDTO> Delete(int id);
        public Task<UserDTO> Get(int id);
        public Task<ICollection<UserDTO>> GetAll();
    }
}
