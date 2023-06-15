using NodaTime;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Domain;

public class TotalFinancesHistory : IIdentityEntity
{
    public long Id { get; set; }

    public Period Period { get; set; }

    public decimal PayrollExpense { get; set; }

    public decimal TotalExpense { get; set; }

    public TotalFinancesHistory(TotalFinances currentTotalFinances, Instant utcNow)
    {
        Period = new Period(currentTotalFinances.CreatedAtUtc, utcNow);
        PayrollExpense = currentTotalFinances.PayrollExpense;
        TotalExpense = currentTotalFinances.TotalExpense;
    }
}