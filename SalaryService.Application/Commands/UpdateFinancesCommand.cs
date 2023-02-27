using NodaTime;
using Period = SalaryService.Domain.Common.Period;
using SalaryService.Domain;
using SalaryService.DataAccess;
using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Commands
{
    public partial class UpdateFinancesCommand
    {
    }

    public class UpdateFinancesCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IClock _clock;

        public UpdateFinancesCommandHandler(EmployeeDbContext employeeDbContext, 
            IClock clock)
        {
            _employeeDbContext = employeeDbContext;
            _clock = clock;
        }

        public async Task HandleAsync(FinanceUpdatingParameters request, EmployeeFinancialMetrics metrics)
        {
            var financeForPayroll = new EmployeeFinanceForPayroll(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.ParkingCostPerMonth);

            var latestMetrics = (await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinancialMetrics;

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
                Prepayment = latestMetrics.Prepayment,
                EmploymentType = latestMetrics.EmploymentType,
                ParkingCostPerMonth = latestMetrics.ParkingCostPerMonth,
                AccountingPerMonth = latestMetrics.AccountingPerMonth
            };

            var currentFinanceForPayroll = (await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinanceForPayroll;

            currentFinanceForPayroll.Update(financeForPayroll.RatePerHour,
                financeForPayroll.Pay.Value,
                financeForPayroll.EmploymentType,
                financeForPayroll.ParkingCostPerMonth);

            var currentFinancialMetrics = (await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinancialMetrics;

            currentFinancialMetrics.Update(metrics.Salary,
                metrics.GrossSalary,
                metrics.NetSalary,
                metrics.DistrictCoefficient,
                metrics.Earnings,
                metrics.IncomeTaxContributions,
                metrics.PensionContributions,
                metrics.MedicalContributions,
                metrics.SocialInsuranceContributions,
                metrics.InjuriesContributions,
                metrics.Expenses,
                metrics.HourlyCostFact,
                metrics.HourlyCostHand,
                metrics.Prepayment,
                metrics.Profit,
                metrics.ProfitAbility,
                metrics.RatePerHour,
                metrics.Pay,
                metrics.EmploymentType,
                metrics.ParkingCostPerMonth,
            _clock.GetCurrentInstant());

            using (var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    _employeeDbContext.Update(currentFinanceForPayroll);
                    _employeeDbContext.Update(currentFinancialMetrics);
                    _employeeDbContext.Add(history);
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
