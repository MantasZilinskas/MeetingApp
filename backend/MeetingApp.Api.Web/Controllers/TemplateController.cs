using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Services.Interfaces;


namespace MeetingApp.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _templateService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _templateService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] TemplateDTO value)
        {
            var result = await _templateService.Insert(value);
            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("Get", result.Id, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TemplateDTO value)
        {
            var result = await _templateService.Update(id, value);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _templateService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
