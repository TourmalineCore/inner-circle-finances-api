using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetSEOAnalyticsInfoListQuery
    {

    }

    public class GetSEOAnalyticsInfoListQueryHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetSEOAnalyticsInfoListQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<IEnumerable<SEOAnalyticsInformationDto>> Handle()
        {
            var employee = await _employeeProfileInfoRepository.GetAllAsync();
            var metrics = await _employeeFinancialMetricsRepository.GetAllAsync();

            var query = from e in employee
                join m in metrics on e.Id equals m.EmployeeId
                select new SEOAnalyticsInformationDto(e.Id,
                    e.Name,
                    e.Surname,
                    e.MiddleName,
                    e.EmploymentDate.ToString(),
                    Math.Round(m.Pay, 2),
                    Math.Round(m.RatePerHour,2),
                    Math.Round(m.EmploymentType, 2),
                    m.HasParking,
                    Math.Round(m.HourlyCostFact, 2),
                    Math.Round(m.HourlyCostHand, 2),
                    Math.Round(m.Earnings, 2),
                    Math.Round(m.Expenses, 2),
                    Math.Round(m.Profit, 2),
                    Math.Round(m.ProfitAbility, 2),
                    Math.Round(m.GrossSalary, 2),
                    Math.Round(m.Retainer, 2),
                    Math.Round(m.NetSalary, 2));

            return query;
        }
    }
}
