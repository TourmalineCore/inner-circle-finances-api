using NodaTime;

namespace SalaryService.Domain;

public class Compensation
{
    public long Id { get; set; }

    public string Type { get; private set; }

    public string Comment { get; private set; }

    public double Amount { get; private set; }

    public Instant CreatedAtUtc { get; private set; }

    public DateOnly Date { get; private set; }

    public Compensation(string type, string comment, double amount, DateOnly date)
    {
        Type = type;
        Comment = comment;
        Amount = amount;
        Date = date;
        CreatedAtUtc = SystemClock.Instance.GetCurrentInstant();
    }
}

