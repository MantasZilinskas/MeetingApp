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
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _service;

        public MeetingController(IMeetingService service)
        {
            _service = service;
        }

        // GET: api/Meeting
        [HttpGet]
        public async Task<ICollection<MeetingDTO>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/Meeting/{id}
        [HttpGet("{id}")]
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
        public async Task<ActionResult> Post(MeetingDTO meeting)
        {
            var returnedValue = await _service.Insert(meeting);
            if(returnedValue == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("Get", new { id = meeting.Id }, returnedValue);
        }

        // PUT api/Meeting/{id}
        [HttpPut("{id}")]
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
        public async Task<ActionResult> Delete(int id)
        {
            var returnedValue = await _service.Delete(id);
            if (returnedValue == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
