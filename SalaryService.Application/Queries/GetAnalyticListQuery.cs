using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.Domain;
using SalaryService.DataAccess;

namespace SalaryService.Application.Queries
{
    public partial class GetAnalyticListQuery
    {

    }

    public class GetAnalyticQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetAnalyticQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<AnalyticDto>> Handle()
        {
            var employee = await _employeeDbContext
                .QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null && x.AccountId != 1)
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();

            return employee.Select(x => new AnalyticDto(x.Id,
                x.Name,
                x.Surname,
                x.MiddleName,
                Math.Round(x.EmployeeFinancialMetrics.Pay, 2),
                Math.Round(x.EmployeeFinancialMetrics.RatePerHour, 2),
                Math.Round(x.EmployeeFinancialMetrics.EmploymentType, 2),
                Math.Round(x.EmployeeFinancialMetrics.ParkingCostPerMonth, 2),
                Math.Round(x.EmployeeFinancialMetrics.HourlyCostFact, 2),
                Math.Round(x.EmployeeFinancialMetrics.HourlyCostHand, 2),
                Math.Round(x.EmployeeFinancialMetrics.Earnings, 2),
                Math.Round(x.EmployeeFinancialMetrics.IncomeTaxContributions, 2),
                Math.Round(x.EmployeeFinancialMetrics.PensionContributions, 2),
                Math.Round(x.EmployeeFinancialMetrics.MedicalContributions, 2),
                Math.Round(x.EmployeeFinancialMetrics.SocialInsuranceContributions, 2),
                Math.Round(x.EmployeeFinancialMetrics.InjuriesContributions, 2),
                Math.Round(x.EmployeeFinancialMetrics.Expenses, 2),
                Math.Round(x.EmployeeFinancialMetrics.Profit, 2),
                Math.Round(x.EmployeeFinancialMetrics.ProfitAbility, 2),
                Math.Round(x.EmployeeFinancialMetrics.GrossSalary, 2),
                Math.Round(x.EmployeeFinancialMetrics.Prepayment, 2),
                Math.Round(x.EmployeeFinancialMetrics.NetSalary, 2)));
        }
    }
}
