using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepo _statusRepo;

        public StatusController(IStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        // GET: api/<StatusController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var statuses = await _statusRepo.GetAllAsync();
            return Ok(statuses);
        }

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var status = _statusRepo.GetByIdAsync(id).Result;
            if (status == null)
            {
                return NotFound("Status Not Found");
            }
            return Ok(status);
        }

        // POST api/<StatusController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Status status)
        {
            var result = await _statusRepo.AddAsync(status);
            return Created($"/status/{status.Id}", result);
        }

        // PUT api/<StatusController>/5
        [HttpPut]
        public async Task<IActionResult> Update(Status status)
        {
            var statusToUpdate = await _statusRepo.GetByIdAsync(status.Id);

            if (statusToUpdate is not null)
            {
                statusToUpdate.Name = status.Name;
                await _statusRepo.UpdateAsync(statusToUpdate);

                return Ok(statusToUpdate);
            }
            return NotFound("No Project Found");
        }
    }
}
