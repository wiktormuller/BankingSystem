using BankingSystem.Shared;
using System.Net;

namespace BankingSystem.Infrastructure.Middleware
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                 BankingSystemException ex =>
                    new ExceptionResponse(new ErrorResponse(new Error(ex.Code, ex.Message)), HttpStatusCode.BadRequest),

                _ =>
                    new ExceptionResponse(new ErrorResponse(new Error("error", "There was an internal error.")),
                        HttpStatusCode.InternalServerError)
            };

        public record ErrorResponse(params Error[] errors);
        public record Error(string Code, string Message);
    }
}
