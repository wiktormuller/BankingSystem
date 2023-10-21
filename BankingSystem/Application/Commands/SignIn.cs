using BankingSystem.Application.Contracts.Responses;
using MediatR;

namespace BankingSystem.Application.Commands
{
    public record SignIn(string Email, string Password) : IRequest<JsonWebTokenResponse>;
}
