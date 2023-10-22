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
    public class BankingAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankingAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankingAccountResponse>> Get([FromQuery] Guid id)
        {
            var bankingAccount = await _mediator.Send(new GetBankingAccount(id));
            if (bankingAccount is not null)
            {
                return Ok(bankingAccount);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddBankingAccountRequest request)
        {
            var userId = Guid.Parse(User.Identity?.Name); // TODO: It could be taken from IContext
            var bankingAccountId = await _mediator.Send(new AddBankingAccount(request.Name, userId));
            return CreatedAtAction(nameof(Get), new { BankingAccountId = bankingAccountId }, null);
        }
    }
}
