using NodaTime;

namespace SalaryService.Domain;

public class Compensation
{
    public long Id { get; set; }

    public string Type { get; private set; }

    public string? Comment { get; private set; }

    public double Amount { get; private set; }

    public bool IsPaid { get; private set; }

    public long EmployeeId { get; private set; }

    public Employee Employee { get; set; }

    public Instant CreatedAtUtc { get; private set; }

    public Instant Date { get; private set; }

    public Compensation(string type, string? comment, double amount, Employee employee, Instant date, bool isPaid = false)
    {
        Type = type;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        Employee = employee;
        CreatedAtUtc = SystemClock.Instance.GetCurrentInstant();
        Date = date;
    }

    public Compensation() { }
}

