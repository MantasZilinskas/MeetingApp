using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingApp.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ITodoItemService _todoItemService;

        public MeetingController(IMeetingService service, ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
            _meetingService = service;
        }

        // GET: api/Meeting
        [HttpGet]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ICollection<MeetingDTO>> Get()
        {
            return await _meetingService.GetAll();
        }

        // GET api/Meeting/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ActionResult<MeetingDTO>> Get(int id)
        {
            var returnedValue = await _meetingService.Get(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return returnedValue;
        }
        // POST api/Meeting
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> Post(MeetingDTO meeting)
        {
            var returnedValue = await _meetingService.Insert(meeting);
            if (returnedValue == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = returnedValue.Id }, returnedValue);
        }

        // PUT api/Meeting/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> Put(int id, MeetingDTO meeting)
        {
            meeting.Id = id;
            var returnedValue = await _meetingService.Update(id, meeting);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/Meeting/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnedValue = await _meetingService.Delete(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Meeting/{meetingId}/TodoItems
        [HttpGet("{meetingId}/TodoItems")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ActionResult> GetMeetingTodoItems(int meetingId)
        {
            var todoItems = await _meetingService.GetMeetingTodoItems(meetingId);
            if(todoItems == null)
            {
                return NotFound();
            }
            return Ok(todoItems);
        }

        // GET: api/Meeting/{meetingId}/TodoItems/{todoItemId}
        [HttpGet("{meetingId}/TodoItems/{todoItemId}")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int meetingId, int todoItemId)
        {
            var returnedValue = await _todoItemService.Get(todoItemId, meetingId);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return returnedValue;
        }

        // POST api/TodoItems
        [HttpPost("{meetingId}/TodoItems/")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> PostTodoItem(int meetingId, TodoItemDTO todoItem)
        {
            todoItem.MeetingId = meetingId;
            var returnedValue = await _todoItemService.Insert(todoItem);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetTodoItem", new { meetingId = meetingId, todoItemId = returnedValue.Id }, returnedValue);
        }

        // PUT api/TodoItems/{id}
        [HttpPut("{meetingId}/TodotItems/{todoItemId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> PutTodoItem(int meetingId, int todoItemId, TodoItemDTO todoItem)
        {
            todoItem.Id = todoItemId;
            todoItem.MeetingId = meetingId;
            var returnedValue = await _todoItemService.Update(todoItemId, todoItem);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/TodoItems/{id}
        [HttpDelete("{meetingId}/TodoItems/{todoItemId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> DeleteTodoItem(int meetingId, int todoItemId)
        {
            var returnedValue = await _todoItemService.Delete(todoItemId, meetingId);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{meetingId}/Users/{userId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> InsertMeetingUser(int meetingId, string userId)
        {
            try
            {
                var returnedUserId = await _meetingService.InsertMeetingUser(userId, meetingId);
                return CreatedAtAction("GetMeetingUser", new
                {
                    meetingId = meetingId,
                    userId = userId
                }, returnedUserId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet("{meetingId}/Users")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult<ICollection<UserResponse>>> GetAllMeetingUsers(int meetingId)
        {
            try
            {
                return Ok(await _meetingService.GetAllMeetingUsers(meetingId));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet("{meetingId}/Users/{userId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> GetMeetingUser(int meetingId, string userId)
        {
            try
            {
                var user = await _meetingService.GetMeetingUser(meetingId, userId);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete("{meetingId}/Users/{userId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> DeleteMeetingUsers(int meetingId, string userId)
        {
            try
            {
                await _meetingService.DeleteMeetingUser(meetingId, userId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
