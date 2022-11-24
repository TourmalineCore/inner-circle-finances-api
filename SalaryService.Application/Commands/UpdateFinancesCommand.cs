using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Services;
using Period = SalaryService.Domain.Common.Period;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateFinancesCommand
    {
    }

    public class UpdateFinancesCommandHandler
    {
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly IClock _clock;
        private readonly CoefficientOptions _coefficientOptions;

        public UpdateFinancesCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository, 
            EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository,
            IClock clock,
            IOptions<CoefficientOptions> coefficientOptions)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _clock = clock;
            _coefficientOptions = coefficientOptions.Value;
        }

        public async Task Handle(long employeeId,
            EmployeeFinanceForPayroll employeeFinanceForPayroll, 
            EmployeeFinancialMetrics employeeFinancialMetrics)
        {
            var latestMetrics = await _employeeFinancialMetricsRepository.GetByEmployeeId(employeeId);
            var history = new EmployeeFinancialMetricsHistory
            {
                EmployeeId = latestMetrics.EmployeeId,
                Period = new Period(latestMetrics.ActualFromUtc, _clock.GetCurrentInstant()),
                Salary = latestMetrics.Salary,
                HourlyCostFact = latestMetrics.HourlyCostFact,
                HourlyCostHand = latestMetrics.HourlyCostHand,
                Earnings = latestMetrics.Earnings,
                IncomeTaxContributions = latestMetrics.IncomeTaxContributions,
                PensionContributions = latestMetrics.PensionContributions,
                MedicalContributions = latestMetrics.MedicalContributions,
                SocialInsuranceContributions = latestMetrics.SocialInsuranceContributions,
                InjuriesContributions = latestMetrics.InjuriesContributions,
                Expenses = latestMetrics.Expenses,
                Profit = latestMetrics.Profit,
                ProfitAbility = latestMetrics.ProfitAbility,
                GrossSalary = latestMetrics.GrossSalary,
                NetSalary = latestMetrics.NetSalary,
                RatePerHour = latestMetrics.RatePerHour,
                Pay = latestMetrics.Pay,
                Retainer = latestMetrics.Retainer,
                EmploymentType = latestMetrics.EmploymentType,
                HasParking = latestMetrics.HasParking,
                ParkingCostPerMonth = latestMetrics.ParkingCostPerMonth,
                AccountingPerMonth = latestMetrics.AccountingPerMonth
            };

            var currentFinanceForPayroll = await _employeeFinanceForPayrollRepository.GetByEmployeeIdAsync(employeeId);
            currentFinanceForPayroll.Update(employeeFinanceForPayroll.RatePerHour, 
                employeeFinanceForPayroll.Pay, 
                employeeFinanceForPayroll.EmploymentType, 
                employeeFinanceForPayroll.HasParking);

            var currentFinancialMetrics = await _employeeFinancialMetricsRepository.GetByEmployeeId(employeeId);
            currentFinancialMetrics.Update(_coefficientOptions.DistrictCoefficient,
                employeeFinancialMetrics.Salary,
                employeeFinancialMetrics.GrossSalary,
                employeeFinancialMetrics.NetSalary,
                employeeFinancialMetrics.Earnings,
                employeeFinancialMetrics.IncomeTaxContributions,
                employeeFinancialMetrics.PensionContributions,
                employeeFinancialMetrics.MedicalContributions,
                employeeFinancialMetrics.SocialInsuranceContributions,
                employeeFinancialMetrics.InjuriesContributions,
                employeeFinancialMetrics.Expenses,
                employeeFinancialMetrics.HourlyCostFact,
                employeeFinancialMetrics.HourlyCostHand,
                employeeFinancialMetrics.Retainer,
                employeeFinancialMetrics.Profit,
                employeeFinancialMetrics.ProfitAbility,
                employeeFinancialMetrics.RatePerHour,
                employeeFinancialMetrics.Pay,
                employeeFinancialMetrics.EmploymentType, 
                employeeFinancialMetrics.HasParking, 
                _clock.GetCurrentInstant());
            
             
            await _employeeFinanceForPayrollRepository.UpdateAsync(currentFinanceForPayroll, currentFinancialMetrics, history);
        }
    }
}
