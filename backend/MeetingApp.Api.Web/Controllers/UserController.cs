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

        [HttpPost("Register")]
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
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var token = await _userService.Login(request.UserName, request.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }
            return Ok(new { token });
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(DeleteRequest request)
        {
            // padaryti deleteByID ir implementuoti update user. Pažiūrėti ar galima useriui skirti keletą rolių
            var result = await _userService.DeleteUser(request.UserName);
            if (result == null)
            {
                return NotFound(result);
            }
            return NoContent();
        }
        
    }
}
