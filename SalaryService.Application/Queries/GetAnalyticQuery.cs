
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetAnalyticQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetAnalyticByIdQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetAnalyticByIdQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<AnalyticDto> Handle(GetAnalyticQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            return new AnalyticDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.HireDate.ToString(),
                Math.Round(employee.EmployeeFinancialMetrics.Pay, 2),
                Math.Round(employee.EmployeeFinancialMetrics.RatePerHour, 2),
                Math.Round(employee.EmployeeFinancialMetrics.EmploymentType, 2),
                Math.Round(employee.EmployeeFinancialMetrics.ParkingCostPerMonth, 2),
                Math.Round(employee.EmployeeFinancialMetrics.HourlyCostFact, 2),
                Math.Round(employee.EmployeeFinancialMetrics.HourlyCostHand, 2),
                Math.Round(employee.EmployeeFinancialMetrics.Earnings, 2),
                Math.Round(employee.EmployeeFinancialMetrics.IncomeTaxContributions, 2),
                Math.Round(employee.EmployeeFinancialMetrics.PensionContributions, 2),
                Math.Round(employee.EmployeeFinancialMetrics.MedicalContributions, 2),
                Math.Round(employee.EmployeeFinancialMetrics.SocialInsuranceContributions, 2),
                Math.Round(employee.EmployeeFinancialMetrics.InjuriesContributions, 2),
                Math.Round(employee.EmployeeFinancialMetrics.Expenses, 2),
                Math.Round(employee.EmployeeFinancialMetrics.Profit, 2),
                Math.Round(employee.EmployeeFinancialMetrics.ProfitAbility, 2),
                Math.Round(employee.EmployeeFinancialMetrics.GrossSalary, 2),
                Math.Round(employee.EmployeeFinancialMetrics.Retainer, 2),
                Math.Round(employee.EmployeeFinancialMetrics.NetSalary, 2));
        }
    }
}
