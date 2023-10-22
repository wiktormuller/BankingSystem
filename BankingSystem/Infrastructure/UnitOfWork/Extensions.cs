using BankingSystem.Infrastructure.Decorators;
using MediatR;

namespace BankingSystem.Infrastructure.UnitOfWork
{
    public static class Extensions
    {
        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
        {
            services.AddScoped<IUnitOfWork, T>();

            services.Decorate(typeof(IRequestHandler<>), typeof(TransactionalCommandHandlerDecorator<>));

            return services;
        }
    }
}
