using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Filters;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingRepo _trackingRepo;

        public TrackingController(ITrackingRepo trackingRepo)
        {
            _trackingRepo = trackingRepo;
        }

        // GET: api/<TrackingController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var trackings = await _trackingRepo.GetAllAsync();
            return Ok(trackings);
        }

        // GET: api/<TrackingController>/filtered
        [Route("Filtered/{filter}")]
        [HttpGet("Filtered/{filter}")]
        public async Task<IActionResult> GetAsync(TrackingFilter _filter)
        {
            var trackings = await _trackingRepo.GetFilteredTrackingsAsync(_filter);
            return Ok(trackings);
        }

        // GET api/<TrackingController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var tracking = _trackingRepo.GetByIdAsync(id).Result;
            if (tracking == null)
            {
                return NotFound("Tracking Not Found");
            }
            return Ok(tracking);
        }

        // POST api/<TrackingController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tracking tracking)
        {
            var result = await _trackingRepo.AddAsync(tracking);
            return Created($"/tracking/{tracking.Id}", result);
        }

        // PUT api/<TrackingController>/5
        [HttpPut]
        public async Task<IActionResult> Update(Tracking tracking)
        {
            var trackingToUpdate = await _trackingRepo.GetByIdAsync(tracking.Id);

            if (trackingToUpdate is not null)
            {
                trackingToUpdate.Subject = tracking.Subject;
                trackingToUpdate.Comments = tracking.Comments;
                trackingToUpdate.CorrespondenceTypeId = tracking.CorrespondenceTypeId;
                trackingToUpdate.PocId = tracking.PocId;
                trackingToUpdate.ToFromId = tracking.ToFromId;
                trackingToUpdate.StatusId = tracking.StatusId;
                trackingToUpdate.ThreadId = tracking.ThreadId;
                trackingToUpdate.DocumentPath = tracking.DocumentPath;
                trackingToUpdate.SentOrReceived = tracking.SentOrReceived;

                await _trackingRepo.UpdateAsync(trackingToUpdate);

                return Ok(trackingToUpdate);
            }
            return NotFound("No Tracking Found");
        }

        // DELETE api/<TrackingController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var trackingToDelete = await _trackingRepo.GetByIdAsync(id);
            if (trackingToDelete == null)
            {
                return NotFound();
            }
            await _trackingRepo.DeleteAsync(trackingToDelete);
            return NoContent();
        }
    }
}
