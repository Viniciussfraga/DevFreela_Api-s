using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers
{
    [Route("api/users")]
    [Authorize] //Exige autorização do token JWT
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // api/users/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var query = new GetUserQuery(id);
            
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }
        // api/users
        [HttpPost]
        [AllowAnonymous] //Permite usar a versão anonima, ou seja, não precisa de autenticação 
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        { 
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {id = id}, command);
        }

        // api/users/login
        [HttpPut("login")]
        [AllowAnonymous] //Permite usar a versão anonima, ou seja, não precisa de autenticação 
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewModel = await _mediator.Send(command);

            if (loginUserViewModel == null) return BadRequest();

            return Ok(loginUserViewModel);
        }

    }
}
