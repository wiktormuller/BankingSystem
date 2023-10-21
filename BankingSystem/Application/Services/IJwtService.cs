using BankingSystem.Application.Services.Models;

namespace BankingSystem.Application.Services
{
    public interface IJwtService
    {
        JsonWebToken CreateToken(string userId, string email, string role);
    }
}