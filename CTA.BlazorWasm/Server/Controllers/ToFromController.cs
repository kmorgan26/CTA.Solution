using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

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
        public async Task<ActionResult<ApiListOfEntityResponse<ToFrom>>> GetAsync()
        {
            try
            {
                var result = await _toFromManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<ToFrom>()
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
        public async Task<ActionResult<ApiListOfEntityResponse<ToFrom>>> GetByToFromId(int id)
        {
            try
            {
                var result = (await _toFromManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
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
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<ToFrom>>> PostAsync([FromBody] ToFrom toFrom)
        {
            try
            {
                await _toFromManager.AddAsync(toFrom);
                var result = (await _toFromManager.GetAsync(i => i.Id == toFrom.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
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
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<ToFrom>>> Update([FromBody] ToFrom toFrom)
        {
            try
            {
                await _toFromManager.UpdateAsync(toFrom);
                var result = (await _toFromManager.GetAsync(i => i.Id == toFrom.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<ToFrom>()
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var toFromList = await _toFromManager.GetAsync(i => i.Id == id);
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
                throw;
            }
        }
    }
}
