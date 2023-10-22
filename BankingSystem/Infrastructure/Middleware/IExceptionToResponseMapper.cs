namespace BankingSystem.Infrastructure.Middleware
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
