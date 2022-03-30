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
    public class ToFromController : ControllerBase
    {
        RepositoryEF<ToFrom, CtaContext> _toFromManager;

        public ToFromController(RepositoryEF<ToFrom, CtaContext> toFromManager)
        {
            _toFromManager = toFromManager;
        }

        // GET: api/<ToFromController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _toFromManager.GetAllAsync();

                return base.Ok(new PagedResponse<ToFrom>()
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

        // GET api/<ToFromController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByToFromId(int id)
        {
            try
            {
                var result = await _toFromManager.dbSet
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "ToFrom Not Found" },
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


        // POST api/<ToFromController>
        [Authorize(Roles = "admin, poweruser")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ToFrom toFrom)
        {
            try
            {
                await _toFromManager.AddAsync(toFrom);

                var result = await _toFromManager.dbSet
                    .Where(i => i.Id == toFrom.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the ToFrom After Adding it. " },
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

        // PUT api/<ToFromController>/5
        [Authorize(Roles = "admin, poweruser")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ToFrom toFrom)
        {
            try
            {
                await _toFromManager.UpdateAsync(toFrom);

                var result = await _toFromManager.dbSet
                    .Where(i => i.Id == toFrom.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<ToFrom>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the ToFrom after updating it" },
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

        // DELETE api/<ToFromController>/5
        [Authorize(Roles = "admin, poweruser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var toFromList = await _toFromManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();

                if (toFromList != null)
                {
                    var toFrom = toFromList.First();
                    var success = await _toFromManager.DeleteAsync(toFrom);
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
            }
        }
    }
}
