using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers {
    [Route("api/projects")]
    [Authorize] //Exige autorização do token JWT
    public class ProjectsController : ControllerBase {

        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        // api/projects?query=net core <--- exemplo
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<ActionResult> Get(string query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // api/projects/id <--- exemplo
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<ActionResult> GetById(int id)
        {
            var getProjectsById = new GetProjectByIdQuery(id);

            var projects = await _mediator.Send(getProjectsById);

            if (projects == null) return NotFound();

            return Ok(projects);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {  
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);   
        }

        // api/projects/id  <--- exemplo
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {

            await _mediator.Send(command);

            return NoContent();
        }
        // api/projects/id  <--- exemplo
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
        // api/projects/id/comments  <--- exemplo
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {     
            await _mediator.Send(command);

            return NoContent();
        }
        // api/projects/id/start  <--- exemplo
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/id/finish  <--- exemplo
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id)
        {
            var command = new FinishProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
