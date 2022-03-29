using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;
using Microsoft.EntityFrameworkCore;
using CTA.BlazorWasm.Shared.Responses;
using Microsoft.AspNetCore.Authorization;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Authorize(Roles ="admin, poweruser")]
    [Route("[controller]")]
    [ApiController]
    public class PocController : ControllerBase
    {
        RepositoryEF<Poc, CtaContext> _pocManager;

        public PocController(RepositoryEF<Poc, CtaContext> pocManager)
        {
            _pocManager = pocManager;
        }

        // GET: api/<PocController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _pocManager.GetAllAsync();

                return Ok(new PagedResponse<Poc>(result)
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

        // GET api/<PocController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByPocId(int id)
        {
            try
            {
                var result = await _pocManager.dbSet
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Poc Not Found" },
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


        // POST api/<PocController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Poc poc)
        {
            try
            {
                await _pocManager.AddAsync(poc);
                var result = await _pocManager.dbSet
                    .Where(i => i.Id == poc.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Poc After Adding it. " },
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

        // PUT api/<PocController>/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Poc poc)
        {
            try
            {
                await _pocManager.UpdateAsync(poc);

                var result = await _pocManager.dbSet
                    .Where(i => i.Id == poc.Id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new Response<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Poc after updating it" },
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

        // DELETE api/<PocController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var pocList = await _pocManager.dbSet
                    .Where(i => i.Id == id)
                    .ToListAsync();
                
                if (pocList != null)
                {
                    var poc = pocList.First();
                    var success = await _pocManager.DeleteAsync(poc);
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
