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
using System.Linq;

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
        public async Task<int> GetCount()
        {
            return await _userManager.Users.CountAsync();
        }
        public async Task<User> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
        public async Task<IdentityResult> Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "StandardUser");
            return result;
        }
        public async Task<IdentityResult> InsertUser(User user, string password, IList<string> roles)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRolesAsync(user, roles);
            return result;
        }

        public async Task<LoginResponseDAO> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Get the role assigned to the user 
                var roles = await _userManager.GetRolesAsync(user);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                AddRolesToClaims(tokenDescriptor, roles);
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var response = new LoginResponseDAO
                {
                    UserId = user.Id,
                    Roles = roles,
                    Token = token
                };
                return response;
            }
            else
            {
                return null;
            }
        }
        private void AddRolesToClaims(SecurityTokenDescriptor tokenDescriptor, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                tokenDescriptor.Subject.AddClaim(roleClaim);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
        public async Task<List<User>> GetSlice(SliceRequestDAO request)
        {
            if (request.order == "asc")
            {
                return request.orderBy switch
                {
                    "username" => await _userManager.Users
                            .OrderBy(i => i.UserName)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "fullname" => await _userManager.Users
                            .OrderBy(i => i.FullName)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "email" => await _userManager.Users
                             .OrderBy(i => i.Email)
                             .Skip(request.rowsPerPage * request.page)
                             .Take(request.rowsPerPage)
                             .ToListAsync(),
                    _ => null
                };
            }
            else
            {
                return request.orderBy switch
                {
                    "username" => await _userManager.Users
                            .OrderByDescending(i => i.UserName)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "fullname" => await _userManager.Users
                            .OrderByDescending(i => i.FullName)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    "email" => await _userManager.Users
                            .OrderByDescending(i => i.Email)
                            .Skip(request.rowsPerPage * request.page)
                            .Take(request.rowsPerPage)
                            .ToListAsync(),
                    _ => null
                };
            }
        }
        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task<IdentityResult> UpdateUser(User user, string userId, string newpass, IList<string> roles)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                throw new KeyNotFoundException();
            }
            var currentRoles = await _userManager.GetRolesAsync(userEntity);
            await _userManager.RemoveFromRolesAsync(userEntity, currentRoles);
            await _userManager.AddToRolesAsync(userEntity, roles);

            var newPassword = _userManager.PasswordHasher.HashPassword(userEntity, newpass);
            userEntity.UserName = user.UserName;
            userEntity.FullName = user.FullName;
            userEntity.Email = user.Email;
            userEntity.PasswordHash = newPassword;


            var result = await _userManager.UpdateAsync(userEntity);
            return result;
        }
        public async Task<IList<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        public async Task<List<Meeting>> GetUserMeetingSlice(string userId, SliceRequestDAO request)
        {
            var user = await _userManager.Users.Include(user => user.Meetings).FirstOrDefaultAsync(user => user.Id == userId);
            if(user != null)
            {
                if (request.order == "asc")
                {
                    return request.orderBy switch
                    {
                        "name" => user.Meetings
                                .OrderBy(i => i.Name)
                                .Skip(request.rowsPerPage * request.page)
                                .Take(request.rowsPerPage)
                                .ToList(),
                        "description" => user.Meetings
                                .OrderBy(i => i.Description)
                                .Skip(request.rowsPerPage * request.page)
                                .Take(request.rowsPerPage)
                                .ToList(),
                        _ => null
                    };
                }
                else
                {
                    return request.orderBy switch
                    {
                        "name" => user.Meetings
                                .OrderByDescending(i => i.Name)
                                .Skip(request.rowsPerPage * request.page)
                                .Take(request.rowsPerPage)
                                .ToList(),
                        "description" => user.Meetings
                                .OrderByDescending(i => i.Description)
                                .Skip(request.rowsPerPage * request.page)
                                .Take(request.rowsPerPage)
                                .ToList(),
                        _ => null
                    };
                }
            }
            return new List<Meeting>();
        }
        public async Task<int> GetUserMeetingCount(string userId)
        {
            return await _userManager.Users.Where(user => user.Id == userId).Select(user => user.Meetings).CountAsync();
        }

    }
}
