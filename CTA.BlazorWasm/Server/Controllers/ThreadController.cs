using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Shared.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private readonly IThreadRepo _threadRepo;

        public ThreadController(IThreadRepo threadRepo)
        {
            _threadRepo = threadRepo;
        }

        //GET: api/<ThreadController>
        [HttpGet("List")]
        public async Task<ActionResult> GetAsync()
        {
            var threads = await _threadRepo.GetAllAsync();
            return Ok(threads);
        }

        // GET api/<ThreadController>/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrackingThread))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var thread = await _threadRepo.GetByIdAsync(id);
            if (thread == null)
            {
                return NotFound("Thread Not Found");
            }
            return Ok(thread);
        }

        // POST api/<ThreadController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TrackingThread trackingThread)
        {
            var result = await _threadRepo.AddAsync(trackingThread);
            return Created($"/project/{result.Id}", result);
        }

        // PUT api/<ThreadController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TrackingThread trackingThread)
        {
            var threadToUpdate = await _threadRepo.GetByIdAsync(trackingThread.Id);

            if(threadToUpdate is not null)
            {
                threadToUpdate.Name = trackingThread.Name;
                await _threadRepo.UpdateAsync(threadToUpdate);
                return Ok(threadToUpdate);
            }
            return NotFound("No Thread Found");
        }

        // DELETE api/<ThreadController>/5
        [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var threadToDelete = await _threadRepo.GetByIdAsync(id);
            if (threadToDelete == null)
            {
                return NotFound();
            }
            await _threadRepo.DeleteAsync(threadToDelete);
            return NoContent();
        }
    }
}
