using NodaTime;
using SalaryService.Domain;
using SalaryService.Tests.Data;

namespace SalaryService.Tests
{
    public class EmployeeFinancialMetricsTest
    {
        private decimal _workingDaysInMonth = (decimal)203 / 12;
        private decimal _workingHoursInMonth => _workingDaysInMonth * 8;

        [Theory]
        [MemberData(nameof(EmployeeFinancialMetricsTestData.Data), MemberType = typeof(EmployeeFinancialMetricsTestData))]
        public void MetricsCalculationIsCorrect(EmployeeFinancialMetrics employeeFinancialMetricsTest,
            decimal ratePerHour,
            decimal pay,
            decimal employeeType,
            decimal parkingCostPerMonth)
        {
            var employeeFinancialMetrics = new EmployeeFinancialMetrics(
                ratePerHour,
                pay,
                employeeType,
                parkingCostPerMonth
            );

            var coefficientOptions = new CoefficientOptions(1, 0.15m, 15279, 0.13m, 49000);

            employeeFinancialMetrics.CalculateMetrics(
               coefficientOptions.DistrictCoefficient,
               coefficientOptions.MinimumWage,
               coefficientOptions.IncomeTaxPercent,
               _workingHoursInMonth,
               new Instant()
            );

            Assert.Equal(employeeFinancialMetricsTest.Salary, employeeFinancialMetrics.Salary);
            Assert.Equal(employeeFinancialMetricsTest.HourlyCostFact, Math.Round(employeeFinancialMetrics.HourlyCostFact, 2));
            Assert.Equal(employeeFinancialMetricsTest.HourlyCostHand, Math.Round(employeeFinancialMetrics.HourlyCostHand, 2));
            Assert.Equal(employeeFinancialMetricsTest.Earnings, Math.Round(employeeFinancialMetrics.Earnings, 2));
            Assert.Equal(employeeFinancialMetricsTest.DistrictCoefficient, Math.Round(employeeFinancialMetrics.DistrictCoefficient, 2));
            Assert.Equal(employeeFinancialMetricsTest.IncomeTaxContributions, Math.Round(employeeFinancialMetrics.IncomeTaxContributions, 2));
            Assert.Equal(employeeFinancialMetricsTest.PensionContributions, Math.Round(employeeFinancialMetrics.PensionContributions, 2));
            Assert.Equal(employeeFinancialMetricsTest.MedicalContributions, Math.Round(employeeFinancialMetrics.MedicalContributions, 2));
            Assert.Equal(employeeFinancialMetricsTest.SocialInsuranceContributions, Math.Round(employeeFinancialMetrics.SocialInsuranceContributions, 2));
            Assert.Equal(employeeFinancialMetricsTest.InjuriesContributions, Math.Round(employeeFinancialMetrics.InjuriesContributions, 2));
            Assert.Equal(employeeFinancialMetricsTest.Expenses, Math.Round(employeeFinancialMetrics.Expenses, 2));
            Assert.Equal(employeeFinancialMetricsTest.Profit, Math.Round(employeeFinancialMetrics.Profit, 2));
            Assert.Equal(employeeFinancialMetricsTest.ProfitAbility, Math.Round(employeeFinancialMetrics.ProfitAbility, 2));
            Assert.Equal(employeeFinancialMetricsTest.GrossSalary, Math.Round(employeeFinancialMetrics.GrossSalary, 2));
            Assert.Equal(employeeFinancialMetricsTest.NetSalary, Math.Round(employeeFinancialMetrics.NetSalary, 2));
            Assert.Equal(employeeFinancialMetricsTest.Prepayment, Math.Round(employeeFinancialMetrics.Prepayment, 2));
            Assert.Equal(employeeFinancialMetricsTest.AccountingPerMonth, Math.Round(employeeFinancialMetrics.AccountingPerMonth, 2));
        }
    }
}