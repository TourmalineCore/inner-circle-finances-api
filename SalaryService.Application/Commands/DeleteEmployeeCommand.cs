using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.DataAccess;
using SalaryService.Domain;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeCommand
    {
        
    }

    public class DeleteEmployeeCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IClock _clock;

        public DeleteEmployeeCommandHandler(EmployeeDbContext employeeDbContext,
            IClock clock)
        {
            _employeeDbContext = employeeDbContext;
            _clock = clock;
        }

        public async Task Handle(long employeeId)
        {
            var employee = await _employeeDbContext
                .Set<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == employeeId && x.DeletedAtUtc == null);

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
                Prepayment = employee.EmployeeFinancialMetrics.Prepayment,
                EmploymentType = employee.EmployeeFinancialMetrics.EmploymentType,
                ParkingCostPerMonth = employee.EmployeeFinancialMetrics.ParkingCostPerMonth,
                AccountingPerMonth = employee.EmployeeFinancialMetrics.AccountingPerMonth
            };

            employee.Delete(_clock.GetCurrentInstant());

            using (var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    _employeeDbContext.Add(history);
                    _employeeDbContext.Remove(employee.EmployeeFinancialMetrics);
                    _employeeDbContext.Remove(employee.EmployeeFinanceForPayroll);
                    _employeeDbContext.Update(employee);
                    transaction.Commit();

                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                }
            }

            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
