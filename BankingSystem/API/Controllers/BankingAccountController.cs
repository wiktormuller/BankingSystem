﻿using BankingSystem.Application.Commands;
using BankingSystem.Application.Contracts.Requests;
using BankingSystem.Application.Contracts.Responses;
using BankingSystem.Application.Queries;
using MediatR;
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

        [HttpGet("{backingAccountId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankingAccountResponse>> Get(Guid backingAccountId)
        {
            var bankingAccount = await _mediator.Send(new GetBankingAccount(backingAccountId));
            if (bankingAccount is not null)
            {
                return Ok(bankingAccount);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddBankingAccountRequest request)
        {
            var bankingAccountId = await _mediator.Send(new AddBankingAccount(request.Name));
            return CreatedAtAction(nameof(Get), new { BankingAccountId = bankingAccountId }, null);
        }
    }
}