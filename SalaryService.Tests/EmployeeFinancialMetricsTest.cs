using NodaTime;
using SalaryService.Domain;

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

        [Fact]
        public void MustPositiveIfRatePerHourIsZero()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
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
                AccountingPerMonth = 600
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(0, 20000, 1, 1800);
            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }

        [Fact]
        public void MustPositiveIfRatePerHourIsEqualOne()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
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
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(1, 20000, 1, 1800);
            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }

        [Fact]
        public void MustPositiveIfRatePerHourIsEqualNormalValue()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
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
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(400, 60000, 1, 1800);
            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }

        [Fact]
        public void MustPositiveIfEmployeeTypeIsHalfOfOne()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
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
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(400, 20000, 0.5, 1800);
            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }

        [Fact]
        public void MustPositiveIfEmployeeTypeIsOne()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
            {
                Salary = 20000,
                HourlyCostFact = 230.45,
                HourlyCostHand = 125.00,
                Earnings = 54133.33,
                DistrictCoefficient = 3000.00,
                IncomeTaxContributions = 2990.00,
                PensionContributions = 4133.48,
                MedicalContributions = 1165.28,
                SocialInsuranceContributions = 443.09,
                InjuriesContributions = 46.00,
                Expenses = 31187.85,
                Profit = 22945.48,
                ProfitAbility = 42.39,
                GrossSalary = 23000.00,
                NetSalary = 20010.00,
                Prepayment = 10005.00,
                AccountingPerMonth = 600
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(400, 20000, 1, 1800);
            employeeFinancialMetrics.CalculateMetrics(
                _coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _coefficientOptions.WorkingHoursInMouth,
                new Instant()
            );

            CheckValues(employeeFinancialMetrics, employeeFinancialMetricsTest);
        }

        [Fact]
        public void MustPositiveIfPayIsOne()
        {
            var employeeFinancialMetricsTest = new EmployeeFinancialMetrics
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
            };

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(400, 1, 1, 1800);
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