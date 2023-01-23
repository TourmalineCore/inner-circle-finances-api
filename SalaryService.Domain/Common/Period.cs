using NodaTime;

namespace SalaryService.Domain.Common;

public class Period
{
    public Instant FromUtc { get; set; }
    public Instant? ToUtc { get; set; }
    public Period(Instant fromUtc, Instant? toUtc)
    {
        FromUtc = fromUtc;
        ToUtc = toUtc;
    }
}