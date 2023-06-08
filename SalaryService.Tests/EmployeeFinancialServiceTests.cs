using Moq;
using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Application.Services;
using SalaryService.Domain;
using SalaryService.Tests.Data;

namespace SalaryService.Tests;

public class EmployeeFinancialServiceTests
{
    private readonly Mock<IFinancialMetricsQuery> _financialMetricsQueryMock;
    private readonly Mock<IEmployeesQuery> _employeesQueryMock;
    private readonly FinancesService _financeService;

    public EmployeeFinancialServiceTests()
    {
        var coefficientsQueryMock = new Mock<ICoefficientsQuery>();
        var workingPlanQueryMock = new Mock<IWorkingPlanQuery>();
        var totalFinancesQueryMock = new Mock<ITotalFinancesQuery>();
        var estimatedFinancialEfficiencyQueryMock = new Mock<IEstimatedFinancialEfficiencyQuery>();

        _financialMetricsQueryMock = new Mock<IFinancialMetricsQuery>();
        _employeesQueryMock = new Mock<IEmployeesQuery>();

        workingPlanQueryMock
            .Setup(x => x.GetWorkingPlanAsync())
            .ReturnsAsync(EmployeeFinancialTestsData.WorkingPlan);

        coefficientsQueryMock
            .Setup(x => x.GetCoefficientsAsync())
            .ReturnsAsync(EmployeeFinancialTestsData.CoefficientOptions);

        _financeService = new FinancesService(
            coefficientsQueryMock.Object,
            _financialMetricsQueryMock.Object,
            workingPlanQueryMock.Object,
            _employeesQueryMock.Object,
            totalFinancesQueryMock.Object,
            estimatedFinancialEfficiencyQueryMock.Object,
            new Clock());
    }

    [Fact]
    public async Task CalculateAnalyticsMetricsChangesAsync_WithEmployeeCopy_CalculationsAreCorrect()
    {
        var metricRows = new List<MetricsRowDto>
        {
            new()
            {
                EmployeeId = "1",
                EmployeeFullName = "lastName name middleName",
                IsCopy = false,
                RatePerHour = 500,
                Pay = 20000,
                EmploymentType = 1,
                ParkingCostPerMonth = 1000,
                IsEmployedOfficially = true
            },
            new()
            {
                EmployeeId = "1_copy",
                EmployeeFullName = "lastName name middleName (Copy)",
                IsCopy = true,
                RatePerHour = 50,
                Pay = 20000,
                EmploymentType = 0.5M,
                ParkingCostPerMonth = 600,
                IsEmployedOfficially = true
            }
        };

        var employeeFinancesForPayroll = new FinancesForPayroll(200, 10000, 0.5m, 500);

        var employeeSourceFinancialMetrics = new EmployeeFinancialMetrics(employeeFinancesForPayroll,
            true,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan,
            It.IsAny<Instant>());

        _financialMetricsQueryMock
            .Setup(x => x.HandleAsync())
            .ReturnsAsync(new List<EmployeeFinancialMetrics> { employeeSourceFinancialMetrics });

        var employee = new Employee("name", "lastName", "middleName", "test@tourmalinecore.com", true) { Id = 1 };
        employee.UpdateFinancialMetrics(employeeFinancesForPayroll,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan,
            It.IsAny<Instant>());

        _employeesQueryMock
            .Setup(x => x.GetEmployeesAsync())
            .ReturnsAsync(new List<Employee>
            {
                employee
            });

        var analyticsMetricChanges = await _financeService.CalculateAnalyticsMetricChangesAsync(metricRows);

        // new metrics are correct for existing employee
        var employeeNewMetrics = analyticsMetricChanges.MetricsRowsChanges[0].NewMetrics;
        Assert.Equal("1", analyticsMetricChanges.MetricsRowsChanges[0].EmployeeId);
        Assert.Equal("lastName name middleName", analyticsMetricChanges.MetricsRowsChanges[0].EmployeeFullName);
        Assert.False(analyticsMetricChanges.MetricsRowsChanges[0].IsCopy);
        Assert.Equal(500, employeeNewMetrics.RatePerHour);
        Assert.Equal(20000, employeeNewMetrics.Pay);
        Assert.Equal(20000, employeeNewMetrics.Salary);
        Assert.Equal(224.54M, Math.Round(employeeNewMetrics.HourlyCostFact, 2));
        Assert.Equal(125, Math.Round(employeeNewMetrics.HourlyCostHand, 2));
        Assert.Equal(67666.67M, Math.Round(employeeNewMetrics.Earnings, 2));
        Assert.Equal(30387.85M, Math.Round(employeeNewMetrics.Expenses, 2));
        Assert.Equal(37278.82M, Math.Round(employeeNewMetrics.Profit, 2));
        Assert.Equal(55.09M, Math.Round(employeeNewMetrics.ProfitAbility, 2));
        Assert.Equal(3000, Math.Round(employeeNewMetrics.DistrictCoefficient, 2));
        Assert.Equal(23000, Math.Round(employeeNewMetrics.GrossSalary, 2));
        Assert.Equal(10005, Math.Round(employeeNewMetrics.Prepayment, 2));
        Assert.Equal(2990, Math.Round(employeeNewMetrics.IncomeTaxContributions, 2));
        Assert.Equal(20010, Math.Round(employeeNewMetrics.NetSalary, 2));
        Assert.Equal(4133.48M, Math.Round(employeeNewMetrics.PensionContributions, 2));
        Assert.Equal(1165.28M, Math.Round(employeeNewMetrics.MedicalContributions, 2));
        Assert.Equal(443.09M, Math.Round(employeeNewMetrics.SocialInsuranceContributions, 2));
        Assert.Equal(46, Math.Round(employeeNewMetrics.InjuriesContributions, 2));
        Assert.Equal(600, Math.Round(employeeNewMetrics.AccountingPerMonth, 2));
        Assert.Equal(1000, employeeNewMetrics.ParkingCostPerMonth);

        // metrics diff are correct for existing employee
        var metricsDiff = analyticsMetricChanges.MetricsRowsChanges[0].MetricsDiff.Value;
        Assert.Equal(300, metricsDiff.RatePerHour);
        Assert.Equal(10000, metricsDiff.Pay);
        Assert.Equal(15000, metricsDiff.Salary);
        Assert.Equal(150.53M, Math.Round(metricsDiff.HourlyCostFact, 2));
        Assert.Equal(93.75M, Math.Round(metricsDiff.HourlyCostHand, 2));
        Assert.Equal(54133.33M, Math.Round(metricsDiff.Earnings, 2));
        Assert.Equal(20372, Math.Round(metricsDiff.Expenses, 2));
        Assert.Equal(33761.33M, Math.Round(metricsDiff.Profit, 2));
        Assert.Equal(29.1M, Math.Round(metricsDiff.ProfitAbility, 2));
        Assert.Equal(2250, Math.Round(metricsDiff.DistrictCoefficient, 2));
        Assert.Equal(17250, Math.Round(metricsDiff.GrossSalary, 2));
        Assert.Equal(7503.75M, Math.Round(metricsDiff.Prepayment, 2));
        Assert.Equal(2242.50M, Math.Round(metricsDiff.IncomeTaxContributions, 2));
        Assert.Equal(15007.50M, Math.Round(metricsDiff.NetSalary, 2));
        Assert.Equal(1725, Math.Round(metricsDiff.PensionContributions, 2));
        Assert.Equal(862.50M, Math.Round(metricsDiff.MedicalContributions, 2));
        Assert.Equal(0, Math.Round(metricsDiff.SocialInsuranceContributions, 2));
        Assert.Equal(34.50M, Math.Round(metricsDiff.InjuriesContributions, 2));
        Assert.Equal(0, Math.Round(metricsDiff.AccountingPerMonth, 2));
        Assert.Equal(500, metricsDiff.ParkingCostPerMonth);

        // new metrics are correct for an employee copy
        Assert.Equal("1_copy", analyticsMetricChanges.MetricsRowsChanges[1].EmployeeId);
        Assert.Equal("lastName name middleName (Copy)", analyticsMetricChanges.MetricsRowsChanges[1].EmployeeFullName);
        Assert.True(analyticsMetricChanges.MetricsRowsChanges[1].IsCopy);
        Assert.Null(analyticsMetricChanges.MetricsRowsChanges[1].MetricsDiff);
        Assert.Equal(20000, analyticsMetricChanges.MetricsRowsChanges[1].NewMetrics.Pay);
        Assert.Equal(50, analyticsMetricChanges.MetricsRowsChanges[1].NewMetrics.RatePerHour);
        Assert.Equal(600, analyticsMetricChanges.MetricsRowsChanges[1].NewMetrics.ParkingCostPerMonth);

        // new total metrics are correct
        var totalSourceMetrics = analyticsMetricChanges.NewTotalMetrics;
        Assert.Equal(71050, Math.Round(totalSourceMetrics.Earnings, 2));
        Assert.Equal(47127.70M, Math.Round(totalSourceMetrics.Expenses, 2));
        Assert.Equal(23922.30M, Math.Round(totalSourceMetrics.Profit, 2));
        Assert.Equal(33.67M, Math.Round(totalSourceMetrics.ProfitAbility, 2));
        Assert.Equal(15007.5M, Math.Round(totalSourceMetrics.Prepayment, 2));
        Assert.Equal(4485, Math.Round(totalSourceMetrics.IncomeTaxContributions, 2));
        Assert.Equal(30015, Math.Round(totalSourceMetrics.NetSalary, 2));
        Assert.Equal(7116.96M, Math.Round(totalSourceMetrics.PensionContributions, 2));
        Assert.Equal(1755.56M, Math.Round(totalSourceMetrics.MedicalContributions, 2));
        Assert.Equal(886.18M, Math.Round(totalSourceMetrics.SocialInsuranceContributions, 2));
        Assert.Equal(69, Math.Round(totalSourceMetrics.InjuriesContributions, 2));
        Assert.Equal(1200, Math.Round(totalSourceMetrics.AccountingPerMonth, 2));
        Assert.Equal(1600, totalSourceMetrics.ParkingCostPerMonth);

        // new total metrics diff is correct
        var totalMetricsDiff = analyticsMetricChanges.TotalMetricsDiff;
        Assert.Equal(57516.67M, Math.Round(totalMetricsDiff.Earnings, 2));
        Assert.Equal(37111.85M, Math.Round(totalMetricsDiff.Expenses, 2));
        Assert.Equal(20404.82M, Math.Round(totalMetricsDiff.Profit, 2));
        Assert.Equal(7.68M, Math.Round(totalMetricsDiff.ProfitAbility, 2));
        Assert.Equal(12506.25M, Math.Round(totalMetricsDiff.Prepayment, 2));
        Assert.Equal(3737.5M, Math.Round(totalMetricsDiff.IncomeTaxContributions, 2));
        Assert.Equal(25012.5M, Math.Round(totalMetricsDiff.NetSalary, 2));
        Assert.Equal(4708.48M, Math.Round(totalMetricsDiff.PensionContributions, 2));
        Assert.Equal(1452.78M, Math.Round(totalMetricsDiff.MedicalContributions, 2));
        Assert.Equal(443.09M, Math.Round(totalMetricsDiff.SocialInsuranceContributions, 2));
        Assert.Equal(57.5M, Math.Round(totalMetricsDiff.InjuriesContributions, 2));
        Assert.Equal(600, Math.Round(totalMetricsDiff.AccountingPerMonth, 2));
        Assert.Equal(1100, totalMetricsDiff.ParkingCostPerMonth);
    }

    [Fact]
    public async Task CalculateAnalyticsMetricsChangesAsync_WithUnofficial_CalculationsAreCorrect()
    {
        var metricRows = new List<MetricsRowDto>
        {
            new()
            {
                EmployeeId = "1",
                EmployeeFullName = "Test Test Test",
                IsCopy = false,
                RatePerHour = 200,
                Pay = 10000,
                EmploymentType = 0.5m,
                ParkingCostPerMonth = 500,
                IsEmployedOfficially = true
            },
            new()
            {
                EmployeeId = "2",
                EmployeeFullName = "lastName name middleName",
                IsCopy = false,
                RatePerHour = 250,
                Pay = 20000,
                EmploymentType = 0.5m,
                ParkingCostPerMonth = 1000,
                IsEmployedOfficially = false
            }
        };

        var employeeFinancesForPayroll = new FinancesForPayroll(200, 10000, 0.5m, 500);

        var employeeSourceFinancialMetrics = new EmployeeFinancialMetrics(employeeFinancesForPayroll,
            true,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan,
            It.IsAny<Instant>());

        var employee2FinancesForPayroll = new FinancesForPayroll(500, 40000, 1m, 2000);

        var employee2SourceFinancialMetrics = new EmployeeFinancialMetrics(employee2FinancesForPayroll,
            true,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan,
            It.IsAny<Instant>());

        _financialMetricsQueryMock
            .Setup(x => x.HandleAsync())
            .ReturnsAsync(new List<EmployeeFinancialMetrics>
                { employeeSourceFinancialMetrics, employee2SourceFinancialMetrics });

        var employee = new Employee("Test", "Test", "Test", "test@tourmalinecore.com", true)
            { Id = 1 };

        employee.UpdateFinancialMetrics(employeeFinancesForPayroll,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan, It.IsAny<Instant>());

        var employee2 = new Employee("name", "lastName", "middleName", "corporate@tourmalinecore.com", true)
            { Id = 2 };

        employee2.UpdateFinancialMetrics(employee2FinancesForPayroll,
            EmployeeFinancialTestsData.CoefficientOptions,
            EmployeeFinancialTestsData.WorkingPlan, It.IsAny<Instant>());

        _employeesQueryMock
            .Setup(x => x.GetEmployeesAsync())
            .ReturnsAsync(new List<Employee>
            {
                employee,
                employee2
            });

        var analyticsMetricChanges = await _financeService.CalculateAnalyticsMetricChangesAsync(metricRows);

        // check that new metrics are correct for unofficial employee
        var employeeNewMetrics = analyticsMetricChanges.MetricsRowsChanges[1].NewMetrics;
        Assert.Equal("2", analyticsMetricChanges.MetricsRowsChanges[1].EmployeeId);
        Assert.Equal("lastName name middleName", analyticsMetricChanges.MetricsRowsChanges[1].EmployeeFullName);
        Assert.False(analyticsMetricChanges.MetricsRowsChanges[1].IsCopy);
        Assert.Equal(250, employeeNewMetrics.RatePerHour);
        Assert.Equal(20000, employeeNewMetrics.Pay);
        Assert.Equal(10000, employeeNewMetrics.Salary);
        Assert.Equal(81.28m, Math.Round(employeeNewMetrics.HourlyCostFact, 2));
        Assert.Equal(62.50m, Math.Round(employeeNewMetrics.HourlyCostHand, 2));
        Assert.Equal(16916.67m, Math.Round(employeeNewMetrics.Earnings, 2));
        Assert.Equal(11000, Math.Round(employeeNewMetrics.Expenses, 2));
        Assert.Equal(5916.67m, Math.Round(employeeNewMetrics.Profit, 2));
        Assert.Equal(34.98m, Math.Round(employeeNewMetrics.ProfitAbility, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.DistrictCoefficient, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.GrossSalary, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.Prepayment, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.IncomeTaxContributions, 2));
        Assert.Equal(10000, Math.Round(employeeNewMetrics.NetSalary, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.PensionContributions, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.MedicalContributions, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.SocialInsuranceContributions, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.InjuriesContributions, 2));
        Assert.Equal(0, Math.Round(employeeNewMetrics.AccountingPerMonth, 2));
        Assert.Equal(1000, employeeNewMetrics.ParkingCostPerMonth);

        // check that metrics diff are correct for unofficial employee
        var metricsDiff = analyticsMetricChanges.MetricsRowsChanges[1].MetricsDiff.Value;
        Assert.Equal(-250, metricsDiff.RatePerHour);
        Assert.Equal(-20000, metricsDiff.Pay);
        Assert.Equal(-30000, metricsDiff.Salary);
        Assert.Equal(-346.43m, Math.Round(metricsDiff.HourlyCostFact, 2));
        Assert.Equal(-187.5m, Math.Round(metricsDiff.HourlyCostHand, 2));
        Assert.Equal(-50750, Math.Round(metricsDiff.Earnings, 2));
        Assert.Equal(-46883.85m, Math.Round(metricsDiff.Expenses, 2));
        Assert.Equal(-3866.15m, Math.Round(metricsDiff.Profit, 2));
        Assert.Equal(20.52m, Math.Round(metricsDiff.ProfitAbility, 2));
        Assert.Equal(-6000, Math.Round(metricsDiff.DistrictCoefficient, 2));
        Assert.Equal(-46000, Math.Round(metricsDiff.GrossSalary, 2));
        Assert.Equal(-20010, Math.Round(metricsDiff.Prepayment, 2));
        Assert.Equal(-5980, Math.Round(metricsDiff.IncomeTaxContributions, 2));
        Assert.Equal(-30020, Math.Round(metricsDiff.NetSalary, 2));
        Assert.Equal(-6433.48m, Math.Round(metricsDiff.PensionContributions, 2));
        Assert.Equal(-2315.28m, Math.Round(metricsDiff.MedicalContributions, 2));
        Assert.Equal(-443.09m, Math.Round(metricsDiff.SocialInsuranceContributions, 2));
        Assert.Equal(-92, Math.Round(metricsDiff.InjuriesContributions, 2));
        Assert.Equal(-600, Math.Round(metricsDiff.AccountingPerMonth, 2));
        Assert.Equal(-1000, metricsDiff.ParkingCostPerMonth);

        // check that new total metrics are correct
        var totalSourceMetrics = analyticsMetricChanges.NewTotalMetrics;
        Assert.Equal(30450, Math.Round(totalSourceMetrics.Earnings, 2));
        Assert.Equal(21015.85m, Math.Round(totalSourceMetrics.Expenses, 2));
        Assert.Equal(9434.15m, Math.Round(totalSourceMetrics.Profit, 2));
        Assert.Equal(30.98m, Math.Round(totalSourceMetrics.ProfitAbility, 2));
        Assert.Equal(2501.25m, Math.Round(totalSourceMetrics.Prepayment, 2));
        Assert.Equal(747.5m, Math.Round(totalSourceMetrics.IncomeTaxContributions, 2));
        Assert.Equal(15002.5m, Math.Round(totalSourceMetrics.NetSalary, 2));
        Assert.Equal(2408.48m, Math.Round(totalSourceMetrics.PensionContributions, 2));
        Assert.Equal(302.78m, Math.Round(totalSourceMetrics.MedicalContributions, 2));
        Assert.Equal(443.09m, Math.Round(totalSourceMetrics.SocialInsuranceContributions, 2));
        Assert.Equal(11.5m, Math.Round(totalSourceMetrics.InjuriesContributions, 2));
        Assert.Equal(600, Math.Round(totalSourceMetrics.AccountingPerMonth, 2));
        Assert.Equal(1500, totalSourceMetrics.ParkingCostPerMonth);

        // check that new total metrics diff is correct
        var totalMetricsDiff = analyticsMetricChanges.TotalMetricsDiff;
        Assert.Equal(-50750, Math.Round(totalMetricsDiff.Earnings, 2));
        Assert.Equal(-46883.85m, Math.Round(totalMetricsDiff.Expenses, 2));
        Assert.Equal(-3866.15m, Math.Round(totalMetricsDiff.Profit, 2));
        Assert.Equal(14.6m, Math.Round(totalMetricsDiff.ProfitAbility, 2));
        Assert.Equal(-20010m, Math.Round(totalMetricsDiff.Prepayment, 2));
        Assert.Equal(-5980m, Math.Round(totalMetricsDiff.IncomeTaxContributions, 2));
        Assert.Equal(-30020m, Math.Round(totalMetricsDiff.NetSalary, 2));
        Assert.Equal(-6433.48m, Math.Round(totalMetricsDiff.PensionContributions, 2));
        Assert.Equal(-2315.28m, Math.Round(totalMetricsDiff.MedicalContributions, 2));
        Assert.Equal(-443.09m, Math.Round(totalMetricsDiff.SocialInsuranceContributions, 2));
        Assert.Equal(-92, Math.Round(totalMetricsDiff.InjuriesContributions, 2));
        Assert.Equal(-600, Math.Round(totalMetricsDiff.AccountingPerMonth, 2));
        Assert.Equal(-1000, totalMetricsDiff.ParkingCostPerMonth);
    }
}