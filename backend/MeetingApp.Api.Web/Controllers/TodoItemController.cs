using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingApp.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: api/TodoItem
        [HttpGet]
        public async Task<ICollection<TodoItemDTO>> Get()
        {
            return await _todoItemService.GetAll();
        }

        // GET api/TodoItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> Get(int id)
        {
            var returnedValue = await _todoItemService.Get(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return returnedValue;
        }

        // POST api/TodoItem
        [HttpPost]
        public async Task<ActionResult> Post(TodoItemDTO todoItem)
        {
            var returnedValue = await _todoItemService.Insert(todoItem);
            if (returnedValue == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = returnedValue.Id }, returnedValue);
        }

        // PUT api/TodoItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TodoItemDTO todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var returnedValue = await _todoItemService.Update(id, todoItem);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/TodoItem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnedValue = await _todoItemService.Delete(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
