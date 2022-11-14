using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetSEOAnalyticsInformationListQuery
    {

    }

    public class GetSEOAnalyticsInformationListQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetSEOAnalyticsInformationListQueryHandler(EmployeeRepository employeeRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<IEnumerable<SEOAnalyticsInformationDto>> Handle()
        {
            var employee = await _employeeRepository.GetAllAsync();
            var metrics = await _employeeFinancialMetricsRepository.GetAllAsync();

            var query = from e in employee
                join m in metrics on e.Id equals m.EmployeeId
                select new SEOAnalyticsInformationDto(e.Id,
                    e.Name,
                    e.Surname,
                    m.Pay,
                    m.RatePerHour,
                    m.EmploymentType,
                    m.HasParking,
                    m.HourlyCostFact,
                    m.HourlyCostHand,
                    m.Earnings,
                    m.Expenses,
                    m.Profit,
                    m.ProfitAbility,
                    m.GrossSalary,
                    m.Retainer,
                    m.NetSalary);

            return query;
        }
    }
}
