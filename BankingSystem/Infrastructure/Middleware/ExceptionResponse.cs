using System.Net;

namespace BankingSystem.Infrastructure.Middleware
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
