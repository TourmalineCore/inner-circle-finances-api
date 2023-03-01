using NodaTime;
using SalaryService.Domain;
using SalaryService.Tests.Data;

namespace SalaryService.Tests
{
    public struct CoefficientOptions
    {
        public decimal DistrictCoefficient = 0.15m;
        public decimal MinimumWage = 15279;
        public decimal IncomeTaxPercent = 0.13m;
        public decimal OfficeExpenses = 49000;
        public decimal WorkingHoursInYear = 247;
        public decimal workingDaysInYearWithoutVacation => WorkingHoursInYear - 24;
        public decimal workingDaysInYearWithoutVacationAndSick => workingDaysInYearWithoutVacation - 20;
        public decimal WorkingDaysInMouth => workingDaysInYearWithoutVacationAndSick / 12;
        public decimal WorkingHoursInMouth => WorkingDaysInMouth * 8;

        public CoefficientOptions()
        {
        }
    }
    
    public class EmployeeFinancialMetricsTest
    {
        private readonly CoefficientOptions _coefficientOptions = new();

        [Theory]
        [MemberData(nameof(EmployeeFinancialMetricsTestData.Data), MemberType = typeof(EmployeeFinancialMetricsTestData))]
        public void Test(EmployeeFinancialMetrics employeeFinancialMetricsTest,
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

            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }
        
        public void CheckValues(EmployeeFinancialMetrics employeeFinancialMetrics, EmployeeFinancialMetrics employeeFinancialMetricsTest)
        {
            Assert.Equal(employeeFinancialMetricsTest.Salary, employeeFinancialMetrics.Salary);
            Assert.Equal(employeeFinancialMetricsTest.HourlyCostFact, Math.Round(employeeFinancialMetrics.HourlyCostFact, 2));
            Assert.Equal(employeeFinancialMetricsTest.HourlyCostHand, Math.Round(employeeFinancialMetrics.HourlyCostHand, 2));
            Assert.Equal(employeeFinancialMetricsTest.Earnings, Math.Round(employeeFinancialMetrics.Earnings, 2)); //Доход
            Assert.Equal(employeeFinancialMetricsTest.DistrictCoefficient, Math.Round(employeeFinancialMetrics.DistrictCoefficient, 2)); //Рай.коэф.
            Assert.Equal(employeeFinancialMetricsTest.IncomeTaxContributions, Math.Round(employeeFinancialMetrics.IncomeTaxContributions, 2)); //НДФЛ
            Assert.Equal(employeeFinancialMetricsTest.PensionContributions, Math.Round(employeeFinancialMetrics.PensionContributions, 2)); //ОПС
            Assert.Equal(employeeFinancialMetricsTest.MedicalContributions, Math.Round(employeeFinancialMetrics.MedicalContributions, 2)); //ОМС
            Assert.Equal(employeeFinancialMetricsTest.SocialInsuranceContributions, Math.Round(employeeFinancialMetrics.SocialInsuranceContributions, 2)); //ОСС
            Assert.Equal(employeeFinancialMetricsTest.InjuriesContributions, Math.Round(employeeFinancialMetrics.InjuriesContributions, 2)); //Взносы на травматизм
            Assert.Equal(employeeFinancialMetricsTest.Expenses, Math.Round(employeeFinancialMetrics.Expenses, 2)); //Расход
            Assert.Equal(employeeFinancialMetricsTest.Profit, Math.Round(employeeFinancialMetrics.Profit, 2)); //Прибыль
            Assert.Equal(employeeFinancialMetricsTest.ProfitAbility, Math.Round(employeeFinancialMetrics.ProfitAbility, 2)); //Рентабельность
            Assert.Equal(employeeFinancialMetricsTest.GrossSalary, Math.Round(employeeFinancialMetrics.GrossSalary, 2)); //Зарплата до вычета НДФЛ
            Assert.Equal(employeeFinancialMetricsTest.NetSalary, Math.Round(employeeFinancialMetrics.NetSalary, 2)); //Зарплата
            Assert.Equal(employeeFinancialMetricsTest.Prepayment, Math.Round(employeeFinancialMetrics.Prepayment, 2)); //Аванас 
            Assert.Equal(employeeFinancialMetricsTest.AccountingPerMonth, Math.Round(employeeFinancialMetrics.AccountingPerMonth, 2));
        }        
    }
}