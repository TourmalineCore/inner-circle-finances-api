
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetSEOAnalyticsInfoQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetSEOAnalyticsInfoQueryHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;
        private readonly  EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetSEOAnalyticsInfoQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<SEOAnalyticsInformationDto> Handle(GetSEOAnalyticsInfoQuery request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.EmployeeId);
            var metrics = await _employeeFinancialMetricsRepository.GetByEmployeeId(request.EmployeeId);

            return new SEOAnalyticsInformationDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.EmploymentDate.ToString(),
                Math.Round(metrics.Pay, 2),
                Math.Round(metrics.RatePerHour, 2),
                metrics.EmploymentType,
                Math.Round(metrics.ParkingCostPerMonth, 2),
                Math.Round(metrics.HourlyCostFact, 2),
                Math.Round(metrics.HourlyCostHand, 2),
                Math.Round(metrics.Earnings, 2),
                Math.Round(metrics.IncomeTaxContributions, 2),
                Math.Round(metrics.PensionContributions, 2),
                Math.Round(metrics.MedicalContributions, 2),
                Math.Round(metrics.SocialInsuranceContributions, 2),
                Math.Round(metrics.InjuriesContributions, 2),
                Math.Round(metrics.Expenses, 2),
                Math.Round(metrics.Profit, 2),
                Math.Round(metrics.ProfitAbility, 2),
                Math.Round(metrics.GrossSalary, 2),
                Math.Round(metrics.Retainer, 2),
                Math.Round(metrics.NetSalary, 2));
        }
    }
}
