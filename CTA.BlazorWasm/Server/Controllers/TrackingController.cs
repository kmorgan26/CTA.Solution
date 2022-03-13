using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using Newtonsoft.Json;
using CTA.BlazorWasm.Shared.Services;
using CTA.BlazorWasm.Shared.Responses;
using CTA.BlazorWasm.Shared.Requests;
using CTA.BlazorWasm.Client.Services;
using CTA.BlazorWasm.Client.ViewModels.Shared;
using CTA.BlazorWasm.Client.Controls.GenericControls;

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
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var paginationFilter = Mapping.Mapper.Map<PaginationFilter>(paginationQuery);
                
                var result = await _trackingManager.GetAllAsync(paginationFilter);

                var paginationResponse = new PagedResponse<Tracking>
                {
                    Data = result.ToList(),
                    Success = true
                };

                return Ok(paginationResponse);
            }
            catch (Exception)
            {
                //TODO: Log Exception
                return StatusCode(500);
            }
        }

        // GET <TrackingController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTrackingId(int id)
        {
            try
            {
                var result = await _trackingManager.dbSet
                    .Include(i => i.ToFrom)
                    .Include(i => i.Status)
                    .Include(i => i.Poc)
                    .Include(I => I.CorrespondenceType)
                    .AsNoTracking()
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Tracking Not Found" },
                        Data = new Tracking()
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // GET <TrackingController>/project/5
        [HttpGet("thread/{id}")]
        public async Task<IActionResult> GetByThreadId(int id)
        {
            try
            {
                var result = await _trackingManager.dbSet
                    .Include(i => i.ToFrom)
                    .Include(i => i.Status)
                    .AsNoTracking()
                    .Where(i => i.ThreadId == id)
                    .ToListAsync();

                if (result != null)
                {
                    var paginatedResponse = new Shared.Responses.PagedResponse<Tracking>
                    {
                        Success = true,
                        Data = result
                    };

                    return Ok(paginatedResponse);
                }
                else
                {
                    return base.Ok(new Shared.Responses.PagedResponse<Tracking>
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Trackings Not Found" },
                        Data = new List<Tracking>()
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // GET<TrackingController>/filter
        [HttpGet("filter/{encodedString}")]
        public async Task<IActionResult> GetTrackingsFiltered(string encodedString)
        {
            try
            {
                var jsonString = await SerializeAndEncode.EncodedStringToJson(encodedString);

                var paginationRequest = System.Text.Json.JsonSerializer.Deserialize<PaginationQuery>(jsonString);

                var filter = paginationRequest.TrackingFilter;

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

                if (filter.StatusId != null && filter.StatusId != 0)
                    result = result.Where(i => i.StatusId == filter.StatusId);

                if (filter.ToFromId != null && filter.ToFromId != 0)
                    result = result.Where(i => i.ToFromId == filter.ToFromId);

                if (filter.CorrTypeId != null && filter.CorrTypeId != 0)
                    result = result.Where(i => i.CorrespondenceTypeId == filter.CorrTypeId);

                if (filter.PocId != null && filter.PocId != 0)
                    result = result.Where(i => i.PocId == filter.PocId);
                
                if (filter.ThreadId != null && filter.ThreadId != 0)
                    result = result.Where(i => i.ThreadId == filter.ThreadId);

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

                if (filter.SubjectText != null)
                    result = result.Where(i => i.Subject.Contains(filter.SubjectText));

                if (filter.CommentsText != null)
                    result = result.Where(i => i.Comments!.Contains(filter.CommentsText));

                var data = await Task.Run(() => result
                    .OrderBy(i => i.ThreadId)
                    .Skip(paginationRequest.PageNumber * paginationRequest.PageSize)
                    .Take(paginationRequest.PageSize)
                    
                    );

                var paginationResponse = new Shared.Responses.PagedResponse<Tracking>
                {
                    Success = true,
                    Data = data.ToList(),
                    TotalRecords = result.Count(),
                    PageNumber = paginationRequest.PageNumber,
                    PageSize = paginationRequest.PageSize,
                };

                return Ok(paginationResponse);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST <TrackingController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tracking tracking)
        {
            try
            {
                await _trackingManager.AddAsync(tracking);

                var result = await _trackingManager.dbSet
                    .Where(i => i.Id == tracking.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Tracking after adding it. Man, I JUST had it! Where did it go?" },
                        Data = new Tracking()
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // PUT <TrackingController>/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Tracking tracking)
        {
            try
            {
                await _trackingManager.UpdateAsync(tracking);
                
                var result = await _trackingManager.dbSet
                    .Where(i => i.Id == tracking.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Tracking>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Tracking after updating it" },
                        Data = new Tracking()
                    });
                }
            }
            catch (Exception)
            {
                // TODO: Log it
                return StatusCode(500);
            }
        }

        // DELETE <TrackingController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var trackingList = await _trackingManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();

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
