using CTA.BlazorWasm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using CTA.BlazorWasm.Server.Data;
using CTA.BlazorWasm.Shared.Data;

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        RepositoryEF<Project, CtaContext> _projectManager;

        public ProjectController(RepositoryEF<Project, CtaContext> projectManager)
        {
            _projectManager = projectManager;
        }

        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<ActionResult<ApiListOfEntityResponse<Project>>> GetAsync()
        {
            try
            {
                var result = await _projectManager.GetAllAsync();
                return Ok(new ApiListOfEntityResponse<Project>()
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

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiListOfEntityResponse<Project>>> GetByProjectId(int id)
        {
            try
            {
                var result = (await _projectManager.GetAsync(x => x.Id == id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Project Not Found" },
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


        // POST api/<ProjectController>
        [HttpPost]
        public async Task<ActionResult<ApiEntityResponse<Project>>> PostAsync([FromBody] Project project)
        {
            try
            {
                await _projectManager.AddAsync(project);
                var result = (await _projectManager.GetAsync(i => i.Id == project.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Project After Adding it. " },
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

        // PUT api/<ProjectController>/5
        [HttpPut]
        public async Task<ActionResult<ApiEntityResponse<Project>>> Update([FromBody] Project project)
        {
            try
            {
                await _projectManager.UpdateAsync(project);
                var result = (await _projectManager.GetAsync(i => i.Id == project.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new ApiEntityResponse<Project>()
                    {
                        Success = false,
                        ErrorMessage = new List<string>() { "Could not find the Project after updating it" },
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

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var projectList = await _projectManager.GetAsync(i => i.Id == id);
                if (projectList != null)
                {
                    var project = projectList.First();
                    var success = await _projectManager.DeleteAsync(project);
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
