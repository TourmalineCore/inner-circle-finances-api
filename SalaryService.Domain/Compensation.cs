using NodaTime;

namespace SalaryService.Domain;

public class Compensation
{
    public long Id { get; set; }

    public long TypeId { get; private set; }

    public string? Comment { get; private set; }

    public double Amount { get; private set; }

    public bool IsPaid { get; set; }

    public long EmployeeId { get; private set; }

    public Employee Employee { get; set; }

    public Instant DateCreateCompensation { get; private set; }

    public Instant DateCompensation { get; private set; }

    public Compensation(long typeId, string? comment, double amount, Employee employee, string dateCompensation, bool isPaid = false)
    {
        if (!CompensationTypes.IsTypeExist(typeId))
        {
            throw new ArgumentException($"Compensation type [{typeId}] doesn't exists");
        }

        if (amount <= 0)
        {
            throw new ArgumentException($"Amount can't be zero or negative");

        }

        TypeId = typeId;
        Comment = comment;     
        Amount = amount;
        IsPaid = isPaid;
        Employee = employee;
        DateCreateCompensation = SystemClock.Instance.GetCurrentInstant();
        DateCompensation = Instant.FromDateTimeUtc(DateTime.SpecifyKind(DateTime.Parse(dateCompensation), DateTimeKind.Utc));
    }

    private Compensation() { }
}
