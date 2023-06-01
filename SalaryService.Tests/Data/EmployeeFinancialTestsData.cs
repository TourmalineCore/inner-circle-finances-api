using Moq;
using NodaTime;
using SalaryService.Domain;

namespace SalaryService.Tests.Data;

public class EmployeeFinancialTestsData
{
    private const decimal WorkingDaysInYear = 247;
    private const decimal WorkingDaysInYearWithoutVacation = WorkingDaysInYear - 24;
    private const decimal WorkingDaysInYearWithoutVacationAndSick = WorkingDaysInYearWithoutVacation - 20;
    private const decimal WorkingDaysInMonth = WorkingDaysInYearWithoutVacationAndSick / 12;
    private const decimal WorkingHoursInMonth = WorkingDaysInMonth * 8;

    public static WorkingPlan WorkingPlan = new(1L,
        WorkingDaysInYear,
        WorkingDaysInYearWithoutVacation,
        WorkingDaysInYearWithoutVacationAndSick,
        WorkingDaysInMonth,
        WorkingHoursInMonth);

    public static CoefficientOptions CoefficientOptions = new(1, 0.15m, 15279, 0.13m, 49000);

    public static IEnumerable<object[]> EmployeeFinancialMetrics()
    {
        yield return new object[]
        {
            new FinancialMetrics(
                new FinancesForPayroll(0, 20000, 1, 1800, true),
                CoefficientOptions,
                WorkingPlan,
                It.IsAny<Instant>())
        };

        yield return new object[]
        {
            new FinancialMetrics(
                new FinancesForPayroll(1, 20000, 1, 1800, true),
                CoefficientOptions,
                WorkingPlan,
                It.IsAny<Instant>())
        };

        yield return new object[]
        {
            new FinancialMetrics(
                new FinancesForPayroll(400, 60000, 1, 1800, true),
                CoefficientOptions,
                WorkingPlan,
                It.IsAny<Instant>())
        };

        yield return new object[]
        {
            new FinancialMetrics(
                new FinancesForPayroll(400, 20000, 0.5m, 1800, true),
                CoefficientOptions,
                WorkingPlan,
                It.IsAny<Instant>())
        };

        yield return new object[]
        {
            new FinancialMetrics(
                new FinancesForPayroll(400, 1, 1, 1800, true),
                CoefficientOptions,
                WorkingPlan,
                It.IsAny<Instant>())
        };
    }
}