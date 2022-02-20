using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTA.BlazorWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepo _projectRepo;

        public ProjectController(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        // GET: api/<ProjectController>
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var projects = await _projectRepo.GetAllAsync();
            return Ok(projects);
        }

        // GET api/<ProjectController>/5
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var project = _projectRepo.GetByIdAsync(id).Result;
            if(project == null)
            {
                return NotFound("Project Not Found");
            }            
            return Ok(project);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Project project)
        {
            var result = await _projectRepo.AddAsync(project);
            return Created($"/project/{project.Id}", result);
        }

        // PUT api/<ProjectController>/5
        [HttpPut]
        public async Task<IActionResult> Update(Project project)
        {
            var projectToUpdate = await _projectRepo.GetByIdAsync(project.Id);

            if(projectToUpdate is not null)
            {
                projectToUpdate.Name = project.Name;
                await _projectRepo.UpdateAsync(projectToUpdate);

                return Ok(projectToUpdate);
            }
            return NotFound("No Project Found");
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var projectToDelete = await _projectRepo.GetByIdAsync(id);
            if(projectToDelete == null)
            {
                return NotFound();
            }
            await _projectRepo.DeleteAsync(projectToDelete);
            return NoContent();
        }
    }
}
