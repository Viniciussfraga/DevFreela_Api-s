using DevFreela.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.Api.Controllers {
    [Route("api/projects")]
    public class ProjectsController : ControllerBase {
        private readonly OpeningTimeOption _option;
        public ProjectsController(IOptions<OpeningTimeOption> option, ExampleClass exampleClass)
        {
            exampleClass.Name = "Updated at ProjectsController";
            _option = option.Value;
        }

        // api/projects?query=net core <--- exemplo
        [HttpGet]
        public ActionResult Get(string query)
        {
            return Ok();
        }

        // api/projects/id <--- exemplo
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            if (createProject.Title.Length > 50) return BadRequest();


            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        // api/projects/id  <--- exemplo
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject.Description.Length > 200) return BadRequest();



            return NoContent();
        }
        // api/projects/id  <--- exemplo
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
        // api/projects/id/comments  <--- exemplo
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
        {
            return NoContent();
        }
        // api/projects/id/start  <--- exemplo
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        // api/projects/id/finish  <--- exemplo
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }
    }
}
