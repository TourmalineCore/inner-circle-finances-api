using System.Data;
using NodaTime;

namespace SalaryService.Domain;

public class TotalFinances
{
    public long Id { get; set; }

    public decimal PayrollExpense { get; private set; }

    public decimal TotalExpense { get; private set; }

    public Instant CreatedAtUtc { get; private set; }

    public TotalFinances()
    {
    }

    public TotalFinances(IEnumerable<FinancialMetrics> metrics, CoefficientOptions coefficients, Instant createdAtUtc)
    {
        PayrollExpense = metrics.Select(x => x.Expenses).Sum();
        TotalExpense = PayrollExpense + coefficients.OfficeExpenses;
        CreatedAtUtc = createdAtUtc;
    }

    public void Update(TotalFinances newTotalFinances)
    {
        PayrollExpense = newTotalFinances.PayrollExpense;
        TotalExpense = newTotalFinances.PayrollExpense;
        CreatedAtUtc = newTotalFinances.CreatedAtUtc;
    }
}

