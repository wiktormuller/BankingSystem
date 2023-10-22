using BankingSystem.Application.Services;

namespace BankingSystem.Infrastructure.Services
{
    public class Clock : IClock
    {
        public DateTime CurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}
