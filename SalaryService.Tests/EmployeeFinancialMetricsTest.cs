using NodaTime;
using SalaryService.Domain;
using SalaryService.Tests.Data;
using System.Collections;

namespace SalaryService.Tests
{
    public struct CoefficientOptions
    {
        public double DistrictCoefficient = 0.15;
        public double MinimumWage = 15279;
        public double IncomeTaxPercent = 0.13;
        public double OfficeExpenses = 49000.0;
        public double WorkingHoursInMouth = 135.3;

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
            double ratePerHour, 
            double pay, 
            double employeeType, 
            double parkingCostPerMonth)
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
            //Assert.Equal(employeeFinancialMetricsTest.HourlyCostFact, employeeFinancialMetrics.HourlyCostFact);
            //Assert.Equal(employeeFinancialMetricsTest.HourlyCostHand, employeeFinancialMetrics.HourlyCostHand);
            //Assert.Equal(employeeFinancialMetricsTest.Earnings, employeeFinancialMetrics.Earnings); //Доход
            Assert.Equal(employeeFinancialMetricsTest.DistrictCoefficient, employeeFinancialMetrics.DistrictCoefficient); //Рай.коэф.
            Assert.Equal(employeeFinancialMetricsTest.IncomeTaxContributions, employeeFinancialMetrics.IncomeTaxContributions); //НДФЛ
            Assert.Equal(employeeFinancialMetricsTest.PensionContributions, employeeFinancialMetrics.PensionContributions); //ОПС
            Assert.Equal(employeeFinancialMetricsTest.MedicalContributions, employeeFinancialMetrics.MedicalContributions); //ОМС
            Assert.Equal(employeeFinancialMetricsTest.SocialInsuranceContributions, employeeFinancialMetrics.SocialInsuranceContributions); //ОСС
            Assert.Equal(employeeFinancialMetricsTest.InjuriesContributions, employeeFinancialMetrics.InjuriesContributions); //Взносы на травматизм
            Assert.Equal(employeeFinancialMetricsTest.Expenses, employeeFinancialMetrics.Expenses); //Расход
            //Assert.Equal(employeeFinancialMetricsTest.Profit, employeeFinancialMetrics.Profit); //Прибыль
            //Assert.Equal(employeeFinancialMetricsTest.ProfitAbility, employeeFinancialMetrics.ProfitAbility); //Рентабельность
            Assert.Equal(employeeFinancialMetricsTest.GrossSalary, employeeFinancialMetrics.GrossSalary); //Зарплата до вычета НДФЛ
            Assert.Equal(employeeFinancialMetricsTest.NetSalary, employeeFinancialMetrics.NetSalary); //Зарплата
            Assert.Equal(employeeFinancialMetricsTest.Prepayment, employeeFinancialMetrics.Prepayment); //Аванас 
            Assert.Equal(employeeFinancialMetricsTest.AccountingPerMonth, employeeFinancialMetrics.AccountingPerMonth);
        }        
    }
}