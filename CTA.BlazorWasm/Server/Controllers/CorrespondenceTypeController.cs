using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

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
        public async Task<ActionResult<ApiListOfEntityResponse<CorrespondenceType>>> GetAsync()
        {
            try
            {
                var result = await _correspondenceTypeManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<CorrespondenceType>()
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

        // GET api/<CorrespondenceTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<CorrespondenceType>>> GetByCorrespondenceTypeId(int id)
        {
            try
            {
                var result = (await _correspondenceTypeManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "CorrespondenceType Not Found" },
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


        // POST api/<CorrespondenceTypeController>
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<CorrespondenceType>>> PostAsync([FromBody] CorrespondenceType correspondenceType)
        {
            try
            {
                await _correspondenceTypeManager.AddAsync(correspondenceType);
                var result = (await _correspondenceTypeManager.GetAsync(i => i.Id == correspondenceType.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the CorrespondenceType After Adding it. " },
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

        // PUT api/<CorrespondenceTypeController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<CorrespondenceType>>> Update([FromBody] CorrespondenceType correspondenceType)
        {
            try
            {
                await _correspondenceTypeManager.UpdateAsync(correspondenceType);
                var result = (await _correspondenceTypeManager.GetAsync(i => i.Id == correspondenceType.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<CorrespondenceType>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the CorrespondenceType after updating it" },
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

        // DELETE api/<CorrespondenceTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var correspondenceTypeList = await _correspondenceTypeManager.GetAsync(i => i.Id == id);
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
