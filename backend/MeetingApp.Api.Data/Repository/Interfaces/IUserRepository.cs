using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> IsDuplicateUsername(User resource);
    }
}
