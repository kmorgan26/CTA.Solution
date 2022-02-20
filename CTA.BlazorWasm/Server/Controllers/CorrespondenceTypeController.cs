using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrespondenceTypeController : ControllerBase
    {
        private readonly ICorrTypeRepo _corrTypeRepo;

        public CorrespondenceTypeController(ICorrTypeRepo corrTypeRepo)
        {
            _corrTypeRepo = corrTypeRepo;
        }

        // GET: api/<CorrespondenceTypeController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var corrTypes = await _corrTypeRepo.GetAllAsync();
            return Ok(corrTypes);
        }

        // GET api/<CorrespondenceTypeController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var correspondenceType = _corrTypeRepo.GetByIdAsync(id).Result;
            if (correspondenceType == null)
            {
                return NotFound("CorrespondenceType Not Found");
            }
            return Ok(correspondenceType);
        }

        // POST api/<CorrespondenceTypeController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CorrespondenceType correspondenceType)
        {
            var result = await _corrTypeRepo.AddAsync(correspondenceType);
            return Created($"/correspondenceType/{correspondenceType.Id}", result);
        }

        // PUT api/<CorrespondenceTypeController>/5
        [HttpPut]
        public async Task<IActionResult> Update(CorrespondenceType correspondenceType)
        {
            var corrTypeToUpdate = await _corrTypeRepo.GetByIdAsync(correspondenceType.Id);

            if (corrTypeToUpdate is not null)
            {
                corrTypeToUpdate.Name = correspondenceType.Name;
                corrTypeToUpdate.CorrespondenceSubTypeId = corrTypeToUpdate.CorrespondenceSubTypeId;

                await _corrTypeRepo.UpdateAsync(corrTypeToUpdate);

                return Ok(corrTypeToUpdate);
            }
            return NotFound("No CorrespondenceType Found");
        }

        // DELETE api/<CorrespondenceTypeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var corrTypeToDelete = await _corrTypeRepo.GetByIdAsync(id);
            if (corrTypeToDelete == null)
            {
                return NotFound();
            }
            await _corrTypeRepo.DeleteAsync(corrTypeToDelete);
            return NoContent();
        }
    }
}
