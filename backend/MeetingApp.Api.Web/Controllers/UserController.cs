using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
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

        // GET: api/User
        [HttpGet]
        public async Task<ICollection<UserDTO>> Get()
        {
            return await _userService.GetAll();
        }

        // GET api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var returnedValue = await _userService.Get(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return returnedValue;
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult> Post(UserDTO user)
        {
            var returnedValue = await _userService.Insert(user);
            if (returnedValue == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = returnedValue.Id }, returnedValue);
        }

        // PUT api/User/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            var returnedValue = await _userService.Update(id, user);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/User/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnedValue = await _userService.Delete(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
