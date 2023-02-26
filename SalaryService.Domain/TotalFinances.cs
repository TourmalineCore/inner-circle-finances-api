using NodaTime;
using SalaryService.Domain;

public class TotalFinances : IIdentityEntity
{
    public long Id { get; set; }

    public Instant ActualFromUtc { get; set; }

    public decimal PayrollExpense { get; set; }

    public decimal TotalExpense { get; set; }

    public TotalFinances()
    {
    }

    public TotalFinances(Instant actualFromUtc)
    {
        ActualFromUtc = actualFromUtc;
    }

    public void CalculateTotals(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients)
    {
        PayrollExpense = metrics.Select(x => x.Expenses).Sum();
        TotalExpense = PayrollExpense + coefficients.OfficeExpenses;
    }

    public void Update(Instant actualFromUtc, decimal payrollExpense, decimal totalExpense)
    {
        ActualFromUtc = actualFromUtc;
        PayrollExpense = payrollExpense;
        TotalExpense = totalExpense;
    }
}