using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

namespace CTA.BlazorWasm.Server.Controllers
{
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
        public async Task<ActionResult<ApiListOfEntityResponse<Poc>>> GetAsync()
        {
            try
            {
                var result = await _pocManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<Poc>()
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

        // GET api/<PocController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Poc>>> GetByPocId(int id)
        {
            try
            {
                var result = (await _pocManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Poc Not Found" },
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


        // POST api/<PocController>
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<Poc>>> PostAsync([FromBody] Poc poc)
        {
            try
            {
                await _pocManager.AddAsync(poc);
                var result = (await _pocManager.GetAsync(i => i.Id == poc.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Poc After Adding it. " },
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

        // PUT api/<PocController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<Poc>>> Update([FromBody] Poc poc)
        {
            try
            {
                await _pocManager.UpdateAsync(poc);
                var result = (await _pocManager.GetAsync(i => i.Id == poc.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Poc>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Poc after updating it" },
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

        // DELETE api/<PocController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var pocList = await _pocManager.GetAsync(i => i.Id == id);
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
