using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> InsertUser(UserRequest user)
        {
            var result = await _userService.InsertUser(user);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRequest user)
        {
            var result = await _userService.Register(user);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var response = await _userService.Login(request.UserName, request.Password);
            if (response == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }
            return Ok(response);
        }
        [HttpGet("slice")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GenericSliceDTO<UserResponse>>>> GetSlice([FromQuery] SliceRequest request)
        {
            var users = await _userService.GetSlice(request);
            if (users == null)
                return BadRequest();
            return Ok(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (result == null)
            {
                return NotFound(result);
            }
            return NoContent();
        }
        
    }
}
