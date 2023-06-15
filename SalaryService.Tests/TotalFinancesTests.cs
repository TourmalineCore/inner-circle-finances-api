using Moq;
using NodaTime;
using SalaryService.Domain;
using SalaryService.Tests.Data;

namespace SalaryService.Tests;

public class TotalFinancesTests
{
    [Fact]
    public void TotalFinancesCalculationIsCorrect()
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

        Assert.Equal(79127.7m, totals.PayrollExpense);
        Assert.Equal(128127.7m, totals.TotalExpense);
    }

    [Fact]
    public void TotalFinancesUpdateIsCorrect()
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
        var newTotals = new TotalFinances(employeeFinancialMetrics, coefficients, It.IsAny<Instant>());

        Assert.Equal(0, totals.PayrollExpense);
        Assert.Equal(49000, totals.TotalExpense);

        totals.Update(newTotals);

        Assert.Equal(79127.7m, totals.PayrollExpense);
        Assert.Equal(128127.7m, totals.TotalExpense);
    }
}