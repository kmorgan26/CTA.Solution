using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/<ThreadController>
        [HttpGet("List/{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var threads = await _threadRepo.GetTrackingThreads(id);
            return Ok(threads);
        }

        // GET api/<ThreadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
