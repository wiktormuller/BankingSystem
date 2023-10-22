using System.Net;

namespace BankingSystem.Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

        public ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionToResponseMapper)
        {
            _exceptionToResponseMapper = exceptionToResponseMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = _exceptionToResponseMapper.Map(exception);

            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

            var response = errorResponse?.Response;
            if (response is null)
            {
                return;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
