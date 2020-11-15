using MeetingApp.Api.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class UserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> InsertUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }


        //public async Task<bool> IsDuplicateUsername(User resource)
        //{
        //    return await _context.Users.AnyAsync(user => user.Username == resource.Username);
        //}
    }
}
