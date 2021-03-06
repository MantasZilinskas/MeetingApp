﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using MeetingApp.Api.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Text.ASCIIEncoding;

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
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        public async Task<ICollection<MeetingDto>> Get()
        {
            return await _meetingService.GetAll();
        }
        // GET: api/Meeting/slice
        [HttpGet("slice")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        public async Task<ActionResult<IEnumerable<GenericSliceDto<MeetingDto>>>> GetSlice([FromQuery] SliceRequest request)
        {
            var meetings = await _meetingService.GetSlice(request);
            if (meetings == null)
                return BadRequest();
            return Ok(meetings);
        }

        // GET api/Meeting/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator + "," + Roles.StandardUser)]
        public async Task<ActionResult<MeetingDto>> Get(int id)
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
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> InsertMeeting(MeetingDto meeting)
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
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> UpdateMeeting(int id, MeetingDto meeting)
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
        [Authorize(Roles = Roles.Moderator)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator + "," + Roles.StandardUser)]
        public async Task<ActionResult> GetMeetingTodoItems(int meetingId)
        {
            var todoItems = await _meetingService.GetMeetingTodoItems(meetingId);
            if (todoItems == null)
            {
                return NotFound();
            }
            return Ok(todoItems);
        }

        // GET: api/Meeting/{meetingId}/TodoItems/{todoItemId}
        [HttpGet("{meetingId}/TodoItems/{todoItemId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator + "," + Roles.StandardUser)]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(int meetingId, int todoItemId)
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
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> PostTodoItem(int meetingId, TodoItemDto todoItem)
        {
            todoItem.MeetingId = meetingId;
            var returnedValue = await _todoItemService.Insert(todoItem);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetTodoItem", new { meetingId, todoItemId = returnedValue.Id }, returnedValue);
        }

        // PUT api/TodoItems/{id}
        [HttpPut("{meetingId}/TodoItems/{todoItemId}")]
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> PutTodoItem(int meetingId, int todoItemId, TodoItemDto todoItem)
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
        [Authorize(Roles = Roles.Moderator)]
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
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> InsertMeetingUser(int meetingId, string userId)
        {
            try
            {
                var returnedUserId = await _meetingService.InsertMeetingUser(userId, meetingId);
                return CreatedAtAction("GetMeetingUser", new
                {
                    meetingId,
                    userId
                }, returnedUserId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet("{meetingId}/Users")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator + "," + Roles.StandardUser)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator + "," + Roles.StandardUser)]
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
        [Authorize(Roles = Roles.Moderator)]
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
        [HttpPut("{meetingId}/texteditor")]
        [Authorize(Roles = Roles.Moderator)]
        public async Task<ActionResult> UpdateMeetingTextEditorData(int meetingId, TextEditorRequest request)
        {
            if (Encoding.Unicode.GetByteCount(request.TextEditorData) >= Convert.ToInt32(Math.Pow(2, 24) - 1))
            {
                return BadRequest(new {error = "Text editor data is too big"});
            }
            var result = await _meetingService.UpdateTextEditorData(meetingId, request.TextEditorData);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

    }
}
