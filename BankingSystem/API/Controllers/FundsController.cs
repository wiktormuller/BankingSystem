using BankingSystem.Application.Commands;
using BankingSystem.Application.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FundsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddFundsRequest request)
        {
            await _mediator.Send(new AddFunds(request.BankingAccountId, request.Amount));
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(WithdrawFundsRequest request)
        {
            await _mediator.Send(new WithdrawFunds(request.BankingAccountId, request.Amount));
            return NoContent();
        }

        [HttpPost("transfer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(TransferFundsRequest request)
        {
            await _mediator.Send(new TransferFunds(request.FromBankingAccountId, request.ToBankingAccountId, request.Amount));
            return NoContent();
        }
    }
}
