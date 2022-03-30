using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        // GET: <StatusController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _statusManager.GetAllAsync();
                return base.Ok(new Shared.Responses.PagedResponse<Status>()
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

        // GET <StatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByStatusId(int id)
        {
            try
            {
                var result = await _statusManager.dbSet
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Status>()
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
        [Authorize(Roles = "admin, poweruser")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Status status)
        {
            try
            {
                await _statusManager.AddAsync(status);

                var result = await _statusManager.dbSet
                    .Where(i => i.Id == status.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Status>()
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
        [Authorize(Roles = "admin, poweruser")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Status status)
        {
            try
            {
                await _statusManager.UpdateAsync(status);

                var result = await _statusManager.dbSet
                    .Where(i => i.Id == status.Id)
                    .FirstOrDefaultAsync();
                
                if (result != null)
                {
                    return Ok(new Response<Status>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Status>()
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
        [Authorize(Roles = "admin, poweruser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var statusList = await _statusManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();

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
