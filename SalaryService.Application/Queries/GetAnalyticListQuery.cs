using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetAnalyticListQuery
    {

    }

    public class GetAnalyticListQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetAnalyticListQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<AnalyticDto>> Handle()
        {
            var employee = await _employeeRepository.GetAllAsync();

            return employee.Select(x => new AnalyticDto(x.Id,
                x.Name,
                x.Surname,
                x.MiddleName,
                x.HireDate.ToString(),
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
                Math.Round(x.EmployeeFinancialMetrics.Retainer, 2),
                Math.Round(x.EmployeeFinancialMetrics.NetSalary, 2)));
        }
    }
}
