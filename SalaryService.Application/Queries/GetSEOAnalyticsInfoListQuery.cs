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
