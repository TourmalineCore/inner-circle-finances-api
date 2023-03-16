using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Application.Commands
{
    public class UpdateFinancesCommand
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

        public async Task HandleAsync(FinanceUpdatingParameters request, EmployeeFinancialMetrics newMetrics)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId);

            if (employee.EmployeeFinancialMetrics == null)
            {
                await CreateNewEmployeeFinancesAsync(request, newMetrics);
            }
            else
            {
                await UpdateEmployeeFinancesAsync(request, newMetrics);
            }
        }

        private async Task CreateNewEmployeeFinancesAsync(FinanceUpdatingParameters request, EmployeeFinancialMetrics newMetrics)
        {
            var employeeFinanceForPayroll = new EmployeeFinanceForPayroll(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.ParkingCostPerMonth,
                request.EmployeeId);

            await using (var transaction = await _employeeDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _employeeDbContext.AddAsync(employeeFinanceForPayroll);
                    await _employeeDbContext.AddAsync(newMetrics);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }

            await _employeeDbContext.SaveChangesAsync();
        }

        // TODO: #861m9k5f6: seems should refactor this method
        private async Task UpdateEmployeeFinancesAsync(FinanceUpdatingParameters request, EmployeeFinancialMetrics newMetrics)
        {
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

            currentFinanceForPayroll.Update(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.ParkingCostPerMonth);

            var currentFinancialMetrics = (await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinancialMetrics;

            currentFinancialMetrics.Update(newMetrics.Salary,
                newMetrics.GrossSalary,
                newMetrics.NetSalary,
                newMetrics.DistrictCoefficient,
                newMetrics.Earnings,
                newMetrics.IncomeTaxContributions,
                newMetrics.PensionContributions,
                newMetrics.MedicalContributions,
                newMetrics.SocialInsuranceContributions,
                newMetrics.InjuriesContributions,
                newMetrics.Expenses,
                newMetrics.HourlyCostFact,
                newMetrics.HourlyCostHand,
                newMetrics.Prepayment,
                newMetrics.Profit,
                newMetrics.ProfitAbility,
                newMetrics.RatePerHour,
                newMetrics.Pay,
                newMetrics.EmploymentType,
                newMetrics.ParkingCostPerMonth,
                _clock.GetCurrentInstant());

            await using (var transaction = await _employeeDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _employeeDbContext.Update(currentFinanceForPayroll);
                    _employeeDbContext.Update(currentFinancialMetrics);
                    await _employeeDbContext.AddAsync(history);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }

            await _employeeDbContext.SaveChangesAsync();
        }
    }
}