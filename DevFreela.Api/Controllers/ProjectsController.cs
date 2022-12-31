using DevFreela.Api.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.Api.Controllers {
    [Route("api/projects")]
    public class ProjectsController : ControllerBase {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService=projectService;
        }

        // api/projects?query=net core <--- exemplo
        [HttpGet]
        public ActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // api/projects/id <--- exemplo
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if(project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            if (inputModel.Title.Length > 50) return BadRequest();

           var id = _projectService.Create(inputModel);
            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);   
        }

        // api/projects/id  <--- exemplo
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 200) return BadRequest();

            _projectService.Update(inputModel);

            return NoContent();
        }
        // api/projects/id  <--- exemplo
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return NoContent();
        }
        // api/projects/id/comments  <--- exemplo
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        {
            _projectService.CreateComment(inputModel);
            return NoContent();
        }
        // api/projects/id/start  <--- exemplo
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        // api/projects/id/finish  <--- exemplo
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }
}
