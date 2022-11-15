using NodaTime;

namespace SalaryService.Domain
{
    public class Clock : IClock
    {
        public Instant GetCurrentInstant()
        {
            return Instant.FromDateTimeUtc(DateTime.UtcNow);
        }
    }
}
