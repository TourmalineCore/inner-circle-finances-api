using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.Domain;
using SalaryService.DataAccess;

namespace SalaryService.Application.Queries;

public class AnalyticsQuery
{
    private readonly EmployeeDbContext _context;

    public AnalyticsQuery(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task<IEnumerable<AnalyticDto>> HandleAsync()
    {
        var employee = await _context
            .QueryableAsNoTracking<Employee>()
            .Where(x => x.DeletedAtUtc == null)
            .Include(x => x.FinancialMetrics)
            .ToListAsync();

        return employee.Select(x => new AnalyticDto(x.Id,
            x.FirstName,
            x.LastName,
            x.MiddleName,
            Math.Round(x.FinancialMetrics.Pay, 2),
            Math.Round(x.FinancialMetrics.RatePerHour, 2),
            Math.Round(x.FinancialMetrics.EmploymentType, 2),
            Math.Round(x.FinancialMetrics.Salary,2),
            Math.Round(x.FinancialMetrics.ParkingCostPerMonth, 2),
            Math.Round(x.FinancialMetrics.AccountingPerMonth, 2),
            Math.Round(x.FinancialMetrics.HourlyCostFact, 2),
            Math.Round(x.FinancialMetrics.HourlyCostHand, 2),
            Math.Round(x.FinancialMetrics.Earnings, 2),
            Math.Round(x.FinancialMetrics.IncomeTaxContributions, 2),
            Math.Round(x.FinancialMetrics.DistrictCoefficient, 2),
            Math.Round(x.FinancialMetrics.PensionContributions, 2),
            Math.Round(x.FinancialMetrics.MedicalContributions, 2),
            Math.Round(x.FinancialMetrics.SocialInsuranceContributions, 2),
            Math.Round(x.FinancialMetrics.InjuriesContributions, 2),
            Math.Round(x.FinancialMetrics.Expenses, 2),
            Math.Round(x.FinancialMetrics.Profit, 2),
            Math.Round(x.FinancialMetrics.ProfitAbility, 2),
            Math.Round(x.FinancialMetrics.GrossSalary, 2),
            Math.Round(x.FinancialMetrics.Prepayment, 2),
            Math.Round(x.FinancialMetrics.NetSalary, 2)));
    }
}
