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
    public class CorrespondenceTypeController : ControllerBase
    {
        RepositoryEF<CorrespondenceType, CtaContext> _correspondenceTypeManager;

        public CorrespondenceTypeController(RepositoryEF<CorrespondenceType, CtaContext> correspondenceTypeManager)
        {
            _correspondenceTypeManager = correspondenceTypeManager;
        }

        // GET: api/<CorrespondenceTypeController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _correspondenceTypeManager.GetAllAsync();

                return Ok(new PagedResponse<CorrespondenceType>()
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

        // GET api/<CorrespondenceTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCorrespondenceTypeId(int id)
        {
            try
            {
                var result = await _correspondenceTypeManager.dbSet
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "CorrespondenceType Not Found" },
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


        // POST api/<CorrespondenceTypeController>
        [Authorize(Roles = "admin, poweruser")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CorrespondenceType correspondenceType)
        {
            try
            {
                await _correspondenceTypeManager.AddAsync(correspondenceType);

                var result = await _correspondenceTypeManager.dbSet
                    .Where(i => i.Id == correspondenceType.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the CorrespondenceType After Adding it. " },
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

        // PUT api/<CorrespondenceTypeController>/5
        [Authorize(Roles = "admin, poweruser")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CorrespondenceType correspondenceType)
        {
            try
            {
                await _correspondenceTypeManager.UpdateAsync(correspondenceType);

                var result = await _correspondenceTypeManager.dbSet
                    .Where(i => i.Id == correspondenceType.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the CorrespondenceType after updating it" },
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

        // DELETE api/<CorrespondenceTypeController>/5
        [Authorize(Roles = "admin, poweruser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var correspondenceTypeList = await _correspondenceTypeManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();

                if (correspondenceTypeList != null)
                {
                    var correspondenceType = correspondenceTypeList.First();
                    var success = await _correspondenceTypeManager.DeleteAsync(correspondenceType);
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
