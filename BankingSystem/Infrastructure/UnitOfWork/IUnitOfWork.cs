﻿namespace BankingSystem.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
        Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken = default);
    }
}
