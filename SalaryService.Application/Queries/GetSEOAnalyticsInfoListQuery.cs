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

        public GetSEOAnalyticsInfoListQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
        }

        public async Task<IEnumerable<SEOAnalyticsInformationDto>> Handle()
        {
            var employee = await _employeeProfileInfoRepository.GetAllAsync();

            return employee.Select(x => new SEOAnalyticsInformationDto(x.Id,
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
