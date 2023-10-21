using MediatR;

namespace BankingSystem.Application.Commands
{
    public record SignUp(string Email, string Password) : IRequest;
}
