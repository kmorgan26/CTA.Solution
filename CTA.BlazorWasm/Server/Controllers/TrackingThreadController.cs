using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

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
        public async Task<ActionResult<ApiListOfEntityResponse<TrackingThread>>> GetAsync()
        {
            try
            {
                var result = await _trackingThreadManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<TrackingThread>()
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception)
            {
                //TODO: Log Exception
                return StatusCode(500);
            }
        }

        // GET <TrackingController>/project/5
        [HttpGet("project/{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<TrackingThread>>> GetByProjectId(int id)
        {
            try
            {
                var result = (await _trackingThreadManager.GetAsync(x => x.ProjectId == id));

                if (result != null)
                {
                    return Ok(new ApiListOfEntityResponse<TrackingThread>()
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
        public async Task<ActionResult<ApiListOfEntityResponse<TrackingThread>>> GetByTrackingThreadId(int id)
        {
            try
            {
                var result = (await _trackingThreadManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "TrackingThread Not Found" },
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
        public async Task<ActionResult<ApiEntityResponse<TrackingThread>>> PostAsync([FromBody] TrackingThread trackingThread)
        {
            try
            {
                await _trackingThreadManager.AddAsync(trackingThread);
                var result = (await _trackingThreadManager.GetAsync(i => i.Id == trackingThread.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the TrackingThread After Adding it. " },
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

        // PUT api/<TrackingThreadController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<TrackingThread>>> Update([FromBody] TrackingThread trackingThread)
        {
            try
            {
                await _trackingThreadManager.UpdateAsync(trackingThread);
                var result = (await _trackingThreadManager.GetAsync(i => i.Id == trackingThread.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<TrackingThread>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the TrackingThread after updating it" },
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
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var trackingThreadList = await _trackingThreadManager.GetAsync(i => i.Id == id);
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
