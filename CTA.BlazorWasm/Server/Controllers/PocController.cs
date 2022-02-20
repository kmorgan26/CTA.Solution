using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty pocs, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PocController : ControllerBase
    {
        private readonly IPocRepo _pocRepo;

        public PocController(IPocRepo pocRepo)
        {
            _pocRepo = pocRepo;
        }

        // GET: api/<PocController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var pocs = await _pocRepo.GetAllAsync();
            return Ok(pocs);
        }

        // GET api/<PocController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var poc = _pocRepo.GetByIdAsync(id).Result;
            if (poc == null)
            {
                return NotFound("Poc Not Found");
            }
            return Ok(poc);
        }

        // POST api/<PocController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Poc poc)
        {
            var result = await _pocRepo.AddAsync(poc);
            return Created($"/poc/{poc.Id}", result);
        }

        // PUT api/<PocController>/5
        [HttpPut]
        public async Task<IActionResult> Update(Poc poc)
        {
            var pocToUpdate = await _pocRepo.GetByIdAsync(poc.Id);

            if (pocToUpdate is not null)
            {
                pocToUpdate.Name = poc.Name;
                pocToUpdate.FirstName = poc.FirstName;
                pocToUpdate.LastName = poc.LastName;
                pocToUpdate.IsActive = poc.IsActive;

                await _pocRepo.UpdateAsync(pocToUpdate);

                return Ok(pocToUpdate);
            }
            return NotFound("No Poc Found");
        }

        // DELETE api/<PocController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var pocToDelete = await _pocRepo.GetByIdAsync(id);
            if (pocToDelete == null)
            {
                return NotFound();
            }
            await _pocRepo.DeleteAsync(pocToDelete);
            return NoContent();
        }
    }
}
