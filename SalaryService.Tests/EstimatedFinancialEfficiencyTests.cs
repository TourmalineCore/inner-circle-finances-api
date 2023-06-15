using Moq;
using NodaTime;
using SalaryService.Domain;
using SalaryService.Tests.Data;

namespace SalaryService.Tests;

public class EstimatedFinancialEfficiencyTests
{
    [Fact]
    public void FinancialEfficiencyCalculationIsCorrect()
    {
        var coefficients = EmployeeFinancialTestsData.CoefficientOptions;
        var workingPlan = EmployeeFinancialTestsData.WorkingPlan;
        var employeeFinancialMetrics = new List<EmployeeFinancialMetrics>
        {
            new (new FinancesForPayroll(400, 20000, 1, 1800), true, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(400, 20000, 0.5m, 0), true, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(400, 20000, 1, 1800), false, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(300, 20000, 0.5m, 0), false, coefficients, workingPlan, It.IsAny<Instant>())
        };

        var totals = new TotalFinances(employeeFinancialMetrics, coefficients, It.IsAny<Instant>());
        var estimatedFinancialEfficiency = new EstimatedFinancialEfficiency(employeeFinancialMetrics, coefficients,
            totals.TotalExpense, It.IsAny<Instant>());

        Assert.Equal(384_383.10m, Math.Round(estimatedFinancialEfficiency.ReserveForQuarter, 2));
        Assert.Equal(768_766.2m, Math.Round(estimatedFinancialEfficiency.ReserveForHalfYear, 2));
        Assert.Equal(1_537_532.40m, Math.Round(estimatedFinancialEfficiency.ReserveForYear, 2));
        Assert.Equal(155_633.33m, Math.Round(estimatedFinancialEfficiency.DesiredEarnings, 2));
        Assert.Equal(27_505.63m, Math.Round(estimatedFinancialEfficiency.DesiredProfit, 2));
        Assert.Equal(17.67m, Math.Round(estimatedFinancialEfficiency.DesiredProfitability, 2));
    }

    [Fact]
    public void FinancialEfficiencyUpdateIsCorrect()
    {
        var coefficients = EmployeeFinancialTestsData.CoefficientOptions;
        var workingPlan = EmployeeFinancialTestsData.WorkingPlan;
        var employeeFinancialMetrics = new List<EmployeeFinancialMetrics>
        {
            new (new FinancesForPayroll(400, 20000, 1, 1800), true, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(400, 20000, 0.5m, 0), true, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(400, 20000, 1, 1800), false, coefficients, workingPlan, It.IsAny<Instant>()),
            new (new FinancesForPayroll(300, 20000, 0.5m, 0), false, coefficients, workingPlan, It.IsAny<Instant>())
        };

        var totals = new TotalFinances(new List<EmployeeFinancialMetrics>(), coefficients, It.IsAny<Instant>());
        var estimatedFinancialEfficiency = new EstimatedFinancialEfficiency(new List<EmployeeFinancialMetrics>(),
            coefficients, totals.TotalExpense, It.IsAny<Instant>());

        var newTotals = new TotalFinances(employeeFinancialMetrics, coefficients, It.IsAny<Instant>());
        var newEstimatedFinancialEfficiency = new EstimatedFinancialEfficiency(employeeFinancialMetrics, coefficients,
            newTotals.TotalExpense, It.IsAny<Instant>());

        Assert.Equal(147_000, Math.Round(estimatedFinancialEfficiency.ReserveForQuarter, 2));
        Assert.Equal(294_000, Math.Round(estimatedFinancialEfficiency.ReserveForHalfYear, 2));
        Assert.Equal(588_000, Math.Round(estimatedFinancialEfficiency.ReserveForYear, 2));
        Assert.Equal(0, Math.Round(estimatedFinancialEfficiency.DesiredEarnings, 2));
        Assert.Equal(-49000, Math.Round(estimatedFinancialEfficiency.DesiredProfit, 2));
        Assert.Equal(-100, Math.Round(estimatedFinancialEfficiency.DesiredProfitability, 2));

        Assert.Equal(0, totals.PayrollExpense);
        Assert.Equal(49000, totals.TotalExpense);

        estimatedFinancialEfficiency.Update(newEstimatedFinancialEfficiency);

        Assert.Equal(384_383.10m, Math.Round(estimatedFinancialEfficiency.ReserveForQuarter, 2));
        Assert.Equal(768_766.2m, Math.Round(estimatedFinancialEfficiency.ReserveForHalfYear, 2));
        Assert.Equal(1_537_532.40m, Math.Round(estimatedFinancialEfficiency.ReserveForYear, 2));
        Assert.Equal(155_633.33m, Math.Round(estimatedFinancialEfficiency.DesiredEarnings, 2));
        Assert.Equal(27_505.63m, Math.Round(estimatedFinancialEfficiency.DesiredProfit, 2));
        Assert.Equal(17.67m, Math.Round(estimatedFinancialEfficiency.DesiredProfitability, 2));
    }
}