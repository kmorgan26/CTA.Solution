using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using LinqKit;

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

        // GET <TrackingController>/project/5
        [HttpGet("thread/{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Tracking>>> GetByThreadId(int id)
        {
            try
            {
                var result = _trackingManager.dbSet
                    .AsNoTracking()
                    .AsExpandable();


                result = result.Where(i => i.ThreadId == id);

                var data = await Task.Run(() => result.OrderBy(i => i.ThreadId));

                if (result != null)
                {
                    return Ok(new ApiListOfEntityResponse<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiListOfEntityResponse<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Trackings Not Found" },
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

        // GET<TrackingController>/filter
        [HttpGet("filtered/{filter}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Tracking>>> GetTrackingsFiltered(TrackingFilter filter)
        {
            try
            {
                var result = _trackingManager.dbSet
                    .Include(i => i.CorrespondenceType)
                        .ThenInclude(j => j.CorrespondenceSubType)
                    .Include(i => i.Thread)
                    .ThenInclude(j => j.Project)
                    .Include(i => i.Status)
                    .Include(i => i.Poc)
                    .Include(i => i.ToFrom)
                    .AsNoTracking()
                    .AsExpandable();

                if (filter.StatusId != null)
                    result = result.Where(i => i.StatusId == filter.StatusId);

                if (filter.ToFromId != null)
                    result = result.Where(i => i.ToFromId == filter.ToFromId);

                if (filter.CorrTypeId != null)
                    result = result.Where(i => i.CorrespondenceTypeId == filter.CorrTypeId);

                if (filter.PocId != null)
                    result = result.Where(i => i.PocId == filter.PocId);

                if (filter.TypeIds != null)
                    result = result.Where(i => filter.TypeIds.Contains(i.CorrespondenceType.CorrespondenceSubTypeId));

                if (filter.StatusIds != null)
                    result = result.Where(i => filter.StatusIds.Contains(i.StatusId));

                if (filter.PocIds != null)
                    result = result.Where(i => filter.PocIds.Contains(i.PocId));

                if (filter.ToFromIds != null)
                    result = result.Where(i => filter.ToFromIds.Contains(i.ToFromId));

                if (filter.StartDate != null && filter.EndDate != null)
                    result = result.Where(i => i.SentOrReceived >= filter.StartDate && i.SentOrReceived <= filter.EndDate);

                if (filter.ThreadId != 0)
                    result = result.Where(i => i.ThreadId == filter.ThreadId);

                if (filter.SubjectText != null)
                    result = result.Where(i => i.Subject.Contains(filter.SubjectText));

                if (filter.CommentsText != null)
                    result = result.Where(i => i.Comments.Contains(filter.CommentsText));

                var data = await Task.Run(() =>  result.OrderBy(i => i.ThreadId));
                
                return Ok(new ApiListOfEntityResponse<Tracking>()
                {
                    Success = true,
                    Data = data
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

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
