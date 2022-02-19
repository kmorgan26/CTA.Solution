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
            throw new NotImplementedException();
        }

        // GET api/<ThreadController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var threads = await _threadRepo.GetTrackingThreads(id);
            if (threads.Any())
            {
                return Ok(threads);
            }
            return BadRequest();
        }

        // POST api/<ThreadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ThreadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ThreadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
