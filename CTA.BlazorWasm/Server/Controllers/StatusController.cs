using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        RepositoryEF<Status, CtaContext> _statusManager;

        public StatusController(RepositoryEF<Status, CtaContext> statusManager)
        {
            _statusManager = statusManager;
        }

        // GET: api/<StatusController>
        [HttpGet]
        public async Task<ActionResult<ApiListOfEntityResponse<Status>>> GetAsync()
        {
            try
            {
                var result = await _statusManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<Status>()
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

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Status>>> GetByStatusId(int id)
        {
            try
            {
                var result = (await _statusManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Status Not Found" },
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


        // POST api/<StatusController>
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<Status>>> PostAsync([FromBody] Status status)
        {
            try
            {
                await _statusManager.AddAsync(status);
                var result = (await _statusManager.GetAsync(i => i.Id == status.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Status After Adding it. " },
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

        // PUT api/<StatusController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<Status>>> Update([FromBody] Status status)
        {
            try
            {
                await _statusManager.UpdateAsync(status);
                var result = (await _statusManager.GetAsync(i => i.Id == status.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Status>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Status after updating it" },
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

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var statusList = await _statusManager.GetAsync(i => i.Id == id);
                if (statusList != null)
                {
                    var status = statusList.First();
                    var success = await _statusManager.DeleteAsync(status);
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
