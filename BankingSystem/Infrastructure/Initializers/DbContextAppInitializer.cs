using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Initializers
{
    public sealed class DbContextAppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextAppInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x)
                    && !x.IsInterface && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();

            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
                if (dbContext is null)
                {
                    continue;
                }

                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
