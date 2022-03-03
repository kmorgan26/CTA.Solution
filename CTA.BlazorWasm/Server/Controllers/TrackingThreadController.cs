using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Requests;
using CTA.BlazorWasm.Client.Services;
using CTA.BlazorWasm.Client.ViewModels.Shared;
using CTA.BlazorWasm.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackingThreadController : ControllerBase
    {
        RepositoryEF<TrackingThread, CtaContext> _trackingThreadManager;

        public TrackingThreadController(RepositoryEF<TrackingThread, CtaContext> trackingThreadManager)
        {
            _trackingThreadManager = trackingThreadManager;
        }

        // GET: api/<TrackingThreadController>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var paginationFilter = Mapping.Mapper.Map<PaginationFilter>(paginationQuery);

                var result = await _trackingThreadManager.GetAllAsync(paginationFilter);

                var paginationResponse = new PagedResponse<TrackingThread>
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

        // GET <TrackingController>/project/5
        [HttpGet("project/{id}")]
        public async Task<IActionResult> GetByProjectId(int id)
        {
            try
            {
                var result = await _trackingThreadManager.dbSet
                    .Where(x => x.ProjectId == id)
                    .AsNoTracking()
                    .ToListAsync();
                    

                if (result != null)
                {
                    return Ok(new PagedResponse<TrackingThread>()
                    {
                        Success = true,
                        Data = result.ToList()
                    });
                }
                else
                {
                    return Ok(new PagedResponse<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Threads Not Found" },
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // GET api/<TrackingThreadController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTrackingThreadId(int id)
        {
            try
            {
                var result = await _trackingThreadManager.dbSet
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Thread Not Found" },
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // POST api/<TrackingThreadController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TrackingThread trackingThread)
        {
            try
            {
                await _trackingThreadManager.AddAsync(trackingThread);

                var result = await _trackingThreadManager.dbSet
                    .Where(i => i.Id == trackingThread.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Thread After Adding it. " },
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception
                return StatusCode(500);
            }
        }

        // PUT <TrackingThreadController>/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TrackingThread trackingThread)
        {
            try
            {
                await _trackingThreadManager.UpdateAsync(trackingThread);
                
                var result = await _trackingThreadManager.dbSet
                    .Where(i => i.Id == trackingThread.Id)
                    .FirstOrDefaultAsync();
                
                if (result != null)
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Thread after updating it" },
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                // TODO: Log it
                return StatusCode(500);
            }
        }

        // DELETE api/<TrackingThreadController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var trackingThreadList = await _trackingThreadManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();
                
                if (trackingThreadList != null)
                {
                    var trackingThread = trackingThreadList.First();
                    var success = await _trackingThreadManager.DeleteAsync(trackingThread);
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
