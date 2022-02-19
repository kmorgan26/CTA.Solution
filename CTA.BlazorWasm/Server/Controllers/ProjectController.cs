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
        public async Task<ActionResult> Get()
        {
            var projects = await _projectRepo.GetAllAsync();
            return Ok(projects);
        }

        // GET api/<ProjectController>/5
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProjectController>/5
        [HttpPut]
        public async Task<IActionResult> Update(Project project)
        {
            var oldProject = await _projectRepo.GetByIdAsync(project.Id);

            if(oldProject is not null)
            {
                oldProject.Name = project.Name;
                await _projectRepo.UpdateAsync(oldProject);

                return Ok(oldProject);
            }
            return NotFound("No Project Found");
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
