using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    //public class UserRepository : GenericRepository<User>, IUserRepository
    //{
    //    public UserRepository(MeetingAppContext context) : base(context) { }

    //    public async Task<bool> IsDuplicateUsername(User resource)
    //    {
    //        return await _context.Users.AnyAsync(user => user.Username == resource.Username);
    //    }
    //}
}
