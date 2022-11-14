
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetSEOAnalyticsInformationQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetSEOAnalyticsInformationQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly  EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetSEOAnalyticsInformationQueryHandler(EmployeeRepository employeeRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<SEOAnalyticsInformationDto> Handle(GetSEOAnalyticsInformationQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            var metrics = await _employeeFinancialMetricsRepository.GetByEmployeeId(request.EmployeeId);

            return new SEOAnalyticsInformationDto(employee.Id,
                employee.Name,
                employee.Surname,
                Math.Round(metrics.Pay, 2),
                Math.Round(metrics.RatePerHour, 2),
                metrics.EmploymentType,
                metrics.HasParking,
                Math.Round(metrics.HourlyCostFact, 2),
                Math.Round(metrics.HourlyCostHand, 2),
                Math.Round(metrics.Earnings, 2),
                Math.Round(metrics.Expenses, 2),
                Math.Round(metrics.Profit, 2),
                Math.Round(metrics.ProfitAbility, 2),
                Math.Round(metrics.GrossSalary, 2),
                Math.Round(metrics.Retainer, 2),
                Math.Round(metrics.NetSalary, 2));
        }
    }
}
