using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Filters;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        RepositoryEF<Tracking, CtaContext> _trackingManager;

        public TrackingController(RepositoryEF<Tracking, CtaContext> trackingManager)
        {
            _trackingManager = trackingManager;
        }

        // GET: <TrackingController>
        [HttpGet]
        public async Task<ActionResult<ApiListOfEntityResponse<Tracking>>> GetAsync()
        {
            try
            {
                var result = await _trackingManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<Tracking>()
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                //TODO: Log Exception
                return StatusCode(500);
            }
        }

        // GET <TrackingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Tracking>>> GetByTrackingId(int id)
        {
            try
            {
                var result = (await _trackingManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if(result != null)
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Tracking Not Found" },
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // GET <TrackingController>/filterString
        //[HttpGet("filter")]
        //public async Task<ActionResult<ApiListOfEntityResponse<Tracking>>> GetTrackingsFiltered(TrackingFilter filter)
        //{
        //    try
        //    {
        //        var result = await _trackingManager
        //            .dbSet
        //            .Include(i => i.CorrespondenceType)
        //                .ThenInclude(j => j.CorrespondenceSubType)
        //            .Include(i => i.Thread)
        //            .ThenInclude(j => j.Project)
        //            .Include(i => i.Status)
        //            .Include(i => i.Poc)
        //            .Include(i => i.ToFrom)
        //            .AsNoTracking();
                
        //        if(filter.StatusId != null)
        //            result = result.Where(i => i.StatusId == filter.StatusId);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        // POST <TrackingController>
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<Tracking>>> PostAsync([FromBody] Tracking tracking)
        {
            try
            {
                await _trackingManager.AddAsync(tracking);
                var result = (await _trackingManager.GetAsync(i => i.Id == tracking.Id)).FirstOrDefault();
                
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Tracking After Adding it. " },
                        Data = null
                    });
                }
            }
            catch(Exception ex)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // PUT <TrackingController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<Tracking>>> Update([FromBody] Tracking tracking)
        {
            try
            {
                await _trackingManager.UpdateAsync(tracking);
                var result = ( await _trackingManager.GetAsync(i => i.Id == tracking.Id)).FirstOrDefault();
                if(result != null)
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Tracking after updating it" },
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                // TODO: Log it
                return StatusCode(500);
            }
        }

        // DELETE <TrackingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var trackingList = await _trackingManager.GetAsync(i => i.Id == id);
                if (trackingList != null)
                {
                    var tracking = trackingList.First();
                    var success = await _trackingManager.DeleteAsync(tracking);
                    if (success)
                        return NoContent();
                    else
                        return StatusCode(500);
                }
                else
                    return StatusCode(500);
            }
            catch (Exception)
            {
                // TODO: Log it
                return StatusCode(500);
                throw;
            }
        }
    }
}
