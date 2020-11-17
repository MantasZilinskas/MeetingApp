using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public UserRepository(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        public async Task<User> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
        public async Task<IdentityResult> InsertUser(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);
            return result;
        }

        public async Task<String> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Get the role assigned to the user 
                var roles = await _userManager.GetRolesAsync(user);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role,string.Join(",", roles))
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return null;
            }
            var result =  await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task<IdentityResult> UpdateUser(User user, string userId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                throw new KeyNotFoundException();
            }
            user.Id = userId;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

    }
}
