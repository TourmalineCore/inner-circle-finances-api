using NodaTime;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeCommand
    {
        
    }

    public class DeleteEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IClock _clock;

        public DeleteEmployeeCommandHandler(EmployeeRepository employeeRepository, 
            IClock clock)
        {
            _employeeRepository = employeeRepository;
            _clock = clock;
        }

        public async Task Handle(long employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            employee.Delete(_clock.GetCurrentInstant());

            var history = new EmployeeFinancialMetricsHistory
            {
                EmployeeId = employee.EmployeeFinancialMetrics.EmployeeId,
                Period = new Period(employee.EmployeeFinancialMetrics.ActualFromUtc, _clock.GetCurrentInstant()),
                Salary = employee.EmployeeFinancialMetrics.Salary,
                HourlyCostFact = employee.EmployeeFinancialMetrics.HourlyCostFact,
                HourlyCostHand = employee.EmployeeFinancialMetrics.HourlyCostHand,
                Earnings = employee.EmployeeFinancialMetrics.Earnings,
                IncomeTaxContributions = employee.EmployeeFinancialMetrics.IncomeTaxContributions,
                PensionContributions = employee.EmployeeFinancialMetrics.PensionContributions,
                MedicalContributions = employee.EmployeeFinancialMetrics.MedicalContributions,
                SocialInsuranceContributions = employee.EmployeeFinancialMetrics.SocialInsuranceContributions,
                InjuriesContributions = employee.EmployeeFinancialMetrics.InjuriesContributions,
                Expenses = employee.EmployeeFinancialMetrics.Expenses,
                Profit = employee.EmployeeFinancialMetrics.Profit,
                ProfitAbility = employee.EmployeeFinancialMetrics.ProfitAbility,
                GrossSalary = employee.EmployeeFinancialMetrics.GrossSalary,
                NetSalary = employee.EmployeeFinancialMetrics.NetSalary,
                RatePerHour = employee.EmployeeFinancialMetrics.RatePerHour,
                Pay = employee.EmployeeFinancialMetrics.Pay,
                Retainer = employee.EmployeeFinancialMetrics.Retainer,
                EmploymentType = employee.EmployeeFinancialMetrics.EmploymentType,
                HasParking = employee.EmployeeFinancialMetrics.HasParking,
                ParkingCostPerMonth = employee.EmployeeFinancialMetrics.ParkingCostPerMonth,
                AccountingPerMonth = employee.EmployeeFinancialMetrics.AccountingPerMonth
            };

            await _employeeRepository.DeleteEmployeeAsync(employee, 
                employee.EmployeeFinanceForPayroll, 
                employee.EmployeeFinancialMetrics, 
                history);
        }
    }
}
