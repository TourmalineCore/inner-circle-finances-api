using NodaTime;
using SalaryService.Domain;

public class TotalFinances : IIdentityEntity
{
    public long Id { get; set; }
    public Instant ActualFromUtc { get; set; }
    public double PayrollExpense { get; set; }
    public double TotalExpense { get; set; }
    public TotalFinances(Instant actualFromUtc)
    {
        ActualFromUtc = actualFromUtc;
    }
    public void CalculateTotals(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients)
    {
        PayrollExpense = metrics.Select(x => x.Expenses).Sum();
        TotalExpense = PayrollExpense + coefficients.OfficeExpenses;
    }
    public void Update(Instant actualFromUtc, double payrollExpense, double totalExpense)
    {
        ActualFromUtc = actualFromUtc;
        PayrollExpense = payrollExpense;
        TotalExpense = totalExpense;
    }
}