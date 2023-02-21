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
                    HourlyCostFact = 230.45,
                    HourlyCostHand = 125.00,
                    Earnings = 0.00,
                    DistrictCoefficient = 3000.00,
                    IncomeTaxContributions = 2990.00,
                    PensionContributions = 4133.48,
                    MedicalContributions = 1165.28,
                    SocialInsuranceContributions = 443.09,
                    InjuriesContributions = 46.00,
                    Expenses = 31187.85,
                    Profit = -31187.85,
                    ProfitAbility = -100.00,
                    GrossSalary = 23000.00,
                    NetSalary = 20010.00,
                    Prepayment = 10005.00,
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
                    HourlyCostFact = 230.45,
                    HourlyCostHand = 125.00,
                    Earnings = 135.33,
                    DistrictCoefficient = 3000.00,
                    IncomeTaxContributions = 2990.00,
                    PensionContributions = 4133.48,
                    MedicalContributions = 1165.28,
                    SocialInsuranceContributions = 443.09,
                    InjuriesContributions = 46.00,
                    Expenses = 31187.85,
                    Profit = -31187.85,
                    ProfitAbility = -22945.21,
                    GrossSalary = 23000.00,
                    NetSalary = 20010.00,
                    Prepayment = 10005.00,
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
                    HourlyCostFact = 622.02,
                    HourlyCostHand = 375.00,
                    Earnings = 54133.33,
                    DistrictCoefficient = 9000.00,
                    IncomeTaxContributions = 8970.00,
                    PensionContributions = 8733.48,
                    MedicalContributions = 3465.28,
                    SocialInsuranceContributions = 443.09,
                    InjuriesContributions = 138.00,
                    Expenses = 84179.85,
                    Profit = -30046.52,
                    ProfitAbility = -55.50,
                    GrossSalary = 69000.00,
                    NetSalary = 60030.00,
                    Prepayment = 30015.00,
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
                    HourlyCostFact = 132.56,
                    HourlyCostHand = 62.50,
                    Earnings = 27066.67,
                    DistrictCoefficient = 1500.00,
                    IncomeTaxContributions = 1495.00,
                    PensionContributions = 2983.48,
                    MedicalContributions = 590.28,
                    SocialInsuranceContributions = 443.09,
                    InjuriesContributions = 23.00,
                    Expenses = 17939.85,
                    Profit = 9126.82,
                    ProfitAbility = 33.72,
                    GrossSalary = 11500.00,
                    NetSalary = 10005.00,
                    Prepayment = 5002.50,
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
                    HourlyCostFact = 34.68,
                    HourlyCostHand = 0.01,
                    Earnings = 54133.33,
                    DistrictCoefficient = 0.15,
                    IncomeTaxContributions = 0.15,
                    PensionContributions = 1833.60,
                    MedicalContributions = 15.34,
                    SocialInsuranceContributions = 443.09,
                    InjuriesContributions = 0.00,
                    Expenses = 4693.18,
                    Profit = 49440.16,
                    ProfitAbility = 91.33,
                    GrossSalary = 1.15,
                    NetSalary = 1.00,
                    Prepayment = 0.50,
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
