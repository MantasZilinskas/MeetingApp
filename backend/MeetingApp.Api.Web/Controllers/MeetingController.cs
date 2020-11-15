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
        private readonly IMeetingService _service;
        private readonly ITodoItemService _todoItemService;

        public MeetingController(IMeetingService service, ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
            _service = service;
        }

        // GET: api/Meeting
        [HttpGet]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ICollection<MeetingDTO>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/Meeting/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ActionResult<MeetingDTO>> Get(int id)
        {
            var returnedValue = await _service.Get(id);
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
            var returnedValue = await _service.Insert(meeting);
            if(returnedValue == null)
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
            if (id != meeting.Id)
            {
                return BadRequest();
            }
            var returnedValue = await _service.Update(id, meeting);
            if(returnedValue == null)
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
            var returnedValue = await _service.Delete(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Meeting/{meetingId}/TodoItems
        [HttpGet("{meetingId}/TodoItems")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ICollection<TodoItemDTO>> GetMeetingTodoItems(int meetingId)
        {
            return await _service.GetMeetingTodoItems(meetingId);
        }

        // GET: api/Meeting/{meetingId}/TodoItems/{todoItemId}
        [HttpGet("{meetingId}/TodoItems/{todoItemId}")]
        [Authorize(Roles = "Admin,StadardUser,Moderator")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int meetingId, int todoItemId)
        {
            var returnedValue = await _todoItemService.Get(todoItemId,meetingId);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return returnedValue;
        }

        // POST api/TodoItems
        [HttpPost("{id}/TodoItems/")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> PostTodoItem(int id, TodoItemDTO todoItem)
        {
            todoItem.MeetingId = id;
            var returnedValue = await _todoItemService.Insert(todoItem);
            if (returnedValue == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = returnedValue.Id }, returnedValue);
        }

        // PUT api/TodoItems/{id}
        [HttpPut("{meetingId}/TodotItems/{todoItemId}")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> PutTodoItem(int meetingId,int todoItemId, TodoItemDTO todoItem)
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
        /*
         * Insert
         * GetAll
         * Update
         * Delete
         */

        //[HttpPost("{meetingId}/Users")]
        //public async Task<ActionResult> PostMeetingUsers(int meetingId, List<UserDTO> users)
        //{
        //    try
        //    {
        //        await _userMeetingService.InsertMeetingUsers(meetingId, users);
        //        return Ok();
        //    }catch(KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpGet("{meetingId}/Users")]
        //public async Task<ActionResult<ICollection<UserDTO>>> GetMeetingUsers(int meetingId)
        //{
        //    try
        //    {
        //        return Ok(await _userMeetingService.GetMeetingUsers(meetingId));
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpPut("{meetingId}/Users")]
        //public async Task<ActionResult> UpdateMeetingUsers(int meetingId, List<UserDTO> users)
        //{
        //    try
        //    {
        //        await _userMeetingService.UpdateMeetingUsers(meetingId, users);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpDelete("{meetingId}/Users")]
        //public async Task<ActionResult> DeleteMeetingUsers(int meetingId)
        //{
        //    try
        //    {
        //        await _userMeetingService.DeleteMeetingUsers(meetingId);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}

    }
}
