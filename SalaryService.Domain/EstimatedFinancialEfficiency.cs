using NodaTime;

namespace SalaryService.Domain;

public class EstimatedFinancialEfficiency
{
    public long Id { get; set; }

    public decimal DesiredEarnings { get; private set; }

    public decimal DesiredProfit { get; private set; }

    public decimal DesiredProfitability { get; private set; }

    public decimal ReserveForQuarter { get; private set; }

    public decimal ReserveForHalfYear { get; private set; }

    public decimal ReserveForYear { get; private set; }

    public Instant CreatedAtUtc { get; private set; }


    private const decimal _desiredProfitabilityWhenZeroDesiredEarnings = -100;

    public EstimatedFinancialEfficiency() { }

    public EstimatedFinancialEfficiency(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, decimal totalExpenses, Instant createdAtUtc)
    {
        DesiredEarnings = metrics.Select(x => x.Earnings).Sum();
        DesiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
        ReserveForQuarter = totalExpenses * 3;
        ReserveForHalfYear = ReserveForQuarter * 2;
        ReserveForYear = ReserveForHalfYear * 2;
        DesiredProfitability = DesiredEarnings != 0
            ? DesiredProfit / DesiredEarnings * 100
            : _desiredProfitabilityWhenZeroDesiredEarnings;
        CreatedAtUtc = createdAtUtc;
    }

    public void Update(EstimatedFinancialEfficiency newEstimatedFinancialEfficiency)
    {
        DesiredEarnings = newEstimatedFinancialEfficiency.DesiredEarnings;
        DesiredProfit = newEstimatedFinancialEfficiency.DesiredProfit;
        ReserveForQuarter = newEstimatedFinancialEfficiency.ReserveForQuarter;
        ReserveForHalfYear = newEstimatedFinancialEfficiency.ReserveForHalfYear;
        ReserveForYear = newEstimatedFinancialEfficiency.ReserveForYear;
        DesiredProfitability = newEstimatedFinancialEfficiency.DesiredProfitability;
        CreatedAtUtc = newEstimatedFinancialEfficiency.CreatedAtUtc;
    }
}

