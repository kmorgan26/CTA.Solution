using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToFromController : ControllerBase
    {
        private readonly IToFromRepo _toFromRepo;

        public ToFromController(IToFromRepo toFromRepo)
        {
            _toFromRepo = toFromRepo;
        }

        // GET: api/<ToFromController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var toFroms = await _toFromRepo.GetAllAsync();
            return Ok(toFroms);
        }

        // GET api/<ToFromController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var toFrom = _toFromRepo.GetByIdAsync(id).Result;
            if (toFrom == null)
            {
                return NotFound("ToFrom Not Found");
            }
            return Ok(toFrom);
        }

        // POST api/<ToFromController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ToFrom toFrom)
        {
            var result = await _toFromRepo.AddAsync(toFrom);
            return Created($"/toFrom/{toFrom.Id}", result);
        }

        // PUT api/<ToFromController>/5
        [HttpPut]
        public async Task<IActionResult> Update(ToFrom toFrom)
        {
            var toFromToUpdate = await _toFromRepo.GetByIdAsync(toFrom.Id);

            if (toFromToUpdate is not null)
            {
                toFromToUpdate.Name = toFrom.Name;
                await _toFromRepo.UpdateAsync(toFromToUpdate);

                return Ok(toFromToUpdate);
            }
            return NotFound("No ToFrom Found");
        }

        // DELETE api/<ToFromController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var toFromToDelete = await _toFromRepo.GetByIdAsync(id);
            if (toFromToDelete == null)
            {
                return NotFound();
            }
            await _toFromRepo.DeleteAsync(toFromToDelete);
            return NoContent();
        }
    }
}
