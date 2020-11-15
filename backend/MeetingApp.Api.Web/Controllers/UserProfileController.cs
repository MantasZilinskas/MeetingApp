using MeetingApp.Api.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,StandardUser,Moderator")]
        public async Task<ActionResult> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userService.GetUserProfile(userId);
            return Ok(user);
        }
        //[Authorize(Roles = "Admin,StandardUser,Moderator")]
        //public async Task<ActionResult> UpdateUserProfile()
        //{

        //}
    }
}
