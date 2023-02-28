using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Tests.Data
{
    public class EmployeeFinancialMetricsTestData
    {
        public static IEnumerable<object[]> Data()
        {
            yield return new object[]
            {
                new EmployeeFinancialMetrics()
                {
                    Salary = 20000,
                    HourlyCostFact = 230.45m,
                    HourlyCostHand = 125.00m,
                    Earnings = 0.00m,
                    DistrictCoefficient = 3000.00m,
                    IncomeTaxContributions = 2990.00m,
                    PensionContributions = 4133.48m,
                    MedicalContributions = 1165.28m,
                    SocialInsuranceContributions = 443.09m,
                    InjuriesContributions = 46.00m,
                    Expenses = 31187.85m,
                    Profit = -31187.85m,
                    ProfitAbility = -100.00m,
                    GrossSalary = 23000.00m,
                    NetSalary = 20010.00m,
                    Prepayment = 10005.00m,
                    AccountingPerMonth = 600,
                },
                0, //ratePerHour
                20000, //pay
                1, //employeeType
                1800 //parkingCostPerMonth
            };

            yield return new object[]
            {
                new EmployeeFinancialMetrics()
                {
                    Salary = 20000,
                    HourlyCostFact = 230.45m,
                    HourlyCostHand = 125.00m,
                    Earnings = 135.33m,
                    DistrictCoefficient = 3000.00m,
                    IncomeTaxContributions = 2990.00m,
                    PensionContributions = 4133.48m,
                    MedicalContributions = 1165.28m,
                    SocialInsuranceContributions = 443.09m,
                    InjuriesContributions = 46.00m,
                    Expenses = 31187.85m,
                    Profit = -31052.52m,
                    ProfitAbility = -22945.21m,
                    GrossSalary = 23000.00m,
                    NetSalary = 20010.00m,
                    Prepayment = 10005.00m,
                    AccountingPerMonth = 600
                },
                1, //ratePerHour
                20000, //pay
                1, //employeeType
                1800 //parkingCostPerMonth
            };

            yield return new object[]
            {
                new EmployeeFinancialMetrics()
                {
                    Salary = 60000,
                    HourlyCostFact = 622.02m,
                    HourlyCostHand = 375.00m,
                    Earnings = 54133.33m,
                    DistrictCoefficient = 9000.00m,
                    IncomeTaxContributions = 8970.00m,
                    PensionContributions = 8733.48m,
                    MedicalContributions = 3465.28m,
                    SocialInsuranceContributions = 443.09m,
                    InjuriesContributions = 138.00m,
                    Expenses = 84179.85m,
                    Profit = -30046.52m,
                    ProfitAbility = -55.50m,
                    GrossSalary = 69000.00m,
                    NetSalary = 60030.00m,
                    Prepayment = 30015.00m,
                    AccountingPerMonth = 600
                },
                400, //ratePerHour
                60000, //pay
                1, //employeeType
                1800 //parkingCostPerMonth
            };

            yield return new object[]
            {
                new EmployeeFinancialMetrics()
                {
                    Salary = 10000,
                    HourlyCostFact = 132.56m,
                    HourlyCostHand = 62.50m,
                    Earnings = 27066.67m,
                    DistrictCoefficient = 1500.00m,
                    IncomeTaxContributions = 1495.00m,
                    PensionContributions = 2983.48m,
                    MedicalContributions = 590.28m,
                    SocialInsuranceContributions = 443.09m,
                    InjuriesContributions = 23.00m,
                    Expenses = 17939.85m,
                    Profit = 9126.82m,
                    ProfitAbility = 33.72m,
                    GrossSalary = 11500.00m,
                    NetSalary = 10005.00m,
                    Prepayment = 5002.50m,
                    AccountingPerMonth = 600
                },
                400, //ratePerHour
                20000, //pay
                0.5, //employeeType
                1800 //parkingCostPerMonth
            };

            yield return new object[]
            {
                new EmployeeFinancialMetrics()
                {
                    Salary = 1,
                    HourlyCostFact = 34.68m,
                    HourlyCostHand = 0.01m,
                    Earnings = 54133.33m,
                    DistrictCoefficient = 0.15m,
                    IncomeTaxContributions = 0.15m,
                    PensionContributions = 1833.60m,
                    MedicalContributions = 15.34m,
                    SocialInsuranceContributions = 443.09m,
                    InjuriesContributions = 0.00m,
                    Expenses = 4693.17m,
                    Profit = 49440.16m,
                    ProfitAbility = 91.33m,
                    GrossSalary = 1.15m,
                    NetSalary = 1.00m,
                    Prepayment = 0.50m,
                    AccountingPerMonth = 600
                },
                400, //ratePerHour
                1, //pay
                1, //employeeType
                1800 //parkingCostPerMonth
            };
        }
    }
}
