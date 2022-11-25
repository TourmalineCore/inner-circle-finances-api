using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Services;
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
        private readonly CoefficientOptions _coefficientOptions;

        public UpdateFinancesCommandHandler(EmployeeDbContext employeeDbContext, 
            IClock clock,
            IOptions<CoefficientOptions> coefficientOptions)
        {
            _employeeDbContext = employeeDbContext;
            _clock = clock;
            _coefficientOptions = coefficientOptions.Value;
        }

        public async Task Handle(FinanceUpdatingParameters request, EmployeeFinancialMetrics metrics)
        {
            var financeForPayroll = new EmployeeFinanceForPayroll(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);

            var latestMetrics = (await _employeeDbContext
                .Set<Employee>()
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
                Retainer = latestMetrics.Retainer,
                EmploymentType = latestMetrics.EmploymentType,
                HasParking = latestMetrics.HasParking,
                ParkingCostPerMonth = latestMetrics.ParkingCostPerMonth,
                AccountingPerMonth = latestMetrics.AccountingPerMonth
            };

            var currentFinanceForPayroll = (await _employeeDbContext
                .Set<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinanceForPayroll;

            currentFinanceForPayroll.Update(financeForPayroll.RatePerHour,
                financeForPayroll.Pay,
                financeForPayroll.EmploymentType,
                financeForPayroll.HasParking);

            var currentFinancialMetrics = (await _employeeDbContext
                .Set<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinancialMetrics;

            currentFinancialMetrics.Update(_coefficientOptions.DistrictCoefficient,
                metrics.Salary,
                metrics.GrossSalary,
                metrics.NetSalary,
                metrics.Earnings,
                metrics.IncomeTaxContributions,
                metrics.PensionContributions,
                metrics.MedicalContributions,
                metrics.SocialInsuranceContributions,
                metrics.InjuriesContributions,
                metrics.Expenses,
                metrics.HourlyCostFact,
                metrics.HourlyCostHand,
                metrics.Retainer,
                metrics.Profit,
                metrics.ProfitAbility,
                metrics.RatePerHour,
                metrics.Pay,
                metrics.EmploymentType,
                metrics.HasParking,
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
