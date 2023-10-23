using BankingSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace BankingSystem.Infrastructure.Decorators
{
    [Decorator]
    public class TransactionalCommandHandlerDecorator<T> : IRequestHandler<T> where T : class, IRequest
    {
        private readonly IRequestHandler<T> _commandHandler;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerDecorator(IRequestHandler<T> commandHandler,
            IUnitOfWork unitOfWork)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(T request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ExecuteAsync(()
                => _commandHandler.Handle(request, cancellationToken));
        }
    }

    [Decorator]
    public class TransactionalCommandHandlerWithResultDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _commandHandler;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerWithResultDecorator(IRequestHandler<TRequest, TResponse> commandHandler,
            IUnitOfWork unitOfWork)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExecuteAsync(()
                => _commandHandler.Handle(request, cancellationToken));
        }
    }
}
