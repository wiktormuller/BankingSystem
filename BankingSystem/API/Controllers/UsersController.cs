using BankingSystem.Application.Commands;
using BankingSystem.Application.Contracts.Requests;
using BankingSystem.Application.Contracts.Responses;
using BankingSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserMeResponse>> Get(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(User.Identity?.Name))
            {
                return NotFound();
            }

            var userId = Guid.Parse(User.Identity?.Name); // TODO: It could be taken from IContext
            var query = new GetUserMe(userId);
            var user = await _mediator.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken)
        {
            var command = new SignUp(request.Email, request.Password);

            await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { command.Email }, null);
        }

        [HttpPost("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonWebTokenResponse>> SignInAsync(SignInRequest request, CancellationToken cancellationToken)
        {
            var command = new SignIn(request.Email, request.Password);
            var jwt = await _mediator.Send(command, cancellationToken);

            return Ok(jwt);
        }
    }
}