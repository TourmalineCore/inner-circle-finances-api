using SalaryService.Domain;

namespace SalaryService.Api.Responses;

public class IndicatorsResponse
{
    public ExpensesDto TotalExpenses { get; private set; }

    public DesiredFinancialMetricsDto DesiredFinancialMetrics { get; private set; }

    public ReserveFinanceDto ReserveFinance { get; private set; }

    public WorkingDaysDto WorkingDays { get; private set; }

    public decimal IncomeTaxPercent { get; private set; }

    public decimal DistrictCoefficient { get; private set; }

    public decimal MinimumWage { get; private set; }

    public IndicatorsResponse(
        CoefficientOptions coefficients, 
        WorkingPlan workingPlan, 
        TotalFinances? totalFinances, 
        EstimatedFinancialEfficiency? estimatedFinancialEfficiency) 
    {
        TotalExpenses = new ExpensesDto(totalFinances, coefficients);
        DesiredFinancialMetrics = new DesiredFinancialMetricsDto(estimatedFinancialEfficiency);
        ReserveFinance = new ReserveFinanceDto(estimatedFinancialEfficiency);
        WorkingDays = new WorkingDaysDto(workingPlan);
        IncomeTaxPercent = coefficients.IncomeTaxPercent;
        DistrictCoefficient = coefficients.DistrictCoefficient;
        MinimumWage = coefficients.MinimumWage;
    }
}

public class ExpensesDto
{
    public decimal? PayrollExpense { get; private set; }
    public decimal OfficeExpense { get; private set; }
    public decimal? TotalExpense { get; private set; }

    public ExpensesDto(TotalFinances? totals, CoefficientOptions coefficients)
    {
        PayrollExpense = totals != null ? Math.Round(totals.PayrollExpense, 2) : null;
        OfficeExpense = Math.Round(coefficients.OfficeExpenses, 2);
        TotalExpense = totals != null ? Math.Round(totals.TotalExpense, 2) : null;
    }
}

public class DesiredFinancialMetricsDto
{
    public decimal? DesiredIncome { get; private set; }
    public decimal? DesiredProfit { get; private set; }
    public decimal? DesiredProfitability { get; private set; }

    public DesiredFinancialMetricsDto(EstimatedFinancialEfficiency? efficiency)
    {
        DesiredIncome = efficiency != null ? Math.Round(efficiency.DesiredEarnings, 2) : null;
        DesiredProfit = efficiency != null ? Math.Round(efficiency.DesiredProfit, 2) : null;
        DesiredProfitability = efficiency != null ? Math.Round(efficiency.DesiredProfitability, 2): null;
    }
}

public class ReserveFinanceDto
{
    public decimal? ReserveForQuarter { get; private set; }
    public decimal? ReserveForHalfYear { get; private set; }
    public decimal? ReserveForYear { get; private set; }

    public ReserveFinanceDto(EstimatedFinancialEfficiency? efficiency)
    {
        ReserveForQuarter = efficiency != null ? Math.Round(efficiency.ReserveForQuarter, 2) : null;
        ReserveForHalfYear = efficiency != null ? Math.Round(efficiency.ReserveForHalfYear, 2) : null;
        ReserveForYear = efficiency != null ? Math.Round(efficiency.ReserveForYear, 2) : null;
    }
}

public class WorkingDaysDto
{
    public decimal WorkingDaysInYear { get; private set; }
    public decimal WorkingDaysInYearWithoutVacation { get; private set; }
    public decimal WorkingDaysInYearWithoutVacationAndSick { get; private set; }
    public decimal WorkingDaysInMonth { get; private set; }
    public decimal WorkingHoursInMonth { get; private set; }

    public WorkingDaysDto(WorkingPlan workingPlan)
    {
        WorkingDaysInYear = workingPlan.WorkingDaysInYear;
        WorkingDaysInYearWithoutVacation = workingPlan.WorkingDaysInYearWithoutVacation;
        WorkingDaysInYearWithoutVacationAndSick = workingPlan.WorkingDaysInYearWithoutVacationAndSick;
        WorkingDaysInMonth = Math.Round(workingPlan.WorkingDaysInMonth, 1);
        WorkingHoursInMonth = Math.Round(workingPlan.WorkingHoursInMonth, 1);
    }
}
