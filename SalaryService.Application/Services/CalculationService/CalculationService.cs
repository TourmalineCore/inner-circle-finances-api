using SalaryService.Application.Dtos;
using SalaryService.Application.Services.HelpServices;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Services.FakeCalculationService
{
    public partial class CalculationServiceParameters
    {
        public long EmployeeId { get; set; }
    }

    public class CalculationService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly BasicSalaryParametersRepository _basicSalaryParametersRepository;
        private readonly FakeTaxService _fakeTaxService;

        public CalculationService(EmployeeRepository employeeRepository, BasicSalaryParametersRepository basicSalaryParametersRepository, FakeTaxService fakeTaxService)
        {
            _employeeRepository = employeeRepository;
            _basicSalaryParametersRepository = basicSalaryParametersRepository;
            _fakeTaxService = fakeTaxService;
        }
        private async Task<EmployeeFinancialMetrics> CalculateEmployeeSalaryMetrics(CalculationServiceParameters parameters)
        {
            var salaryData = _basicSalaryParametersRepository.GetBasicSalaryParametersByEmployeeIdAsync(parameters.EmployeeId).Result;
            var calculatedSalaryData = new EmployeeFinancialMetrics(parameters.EmployeeId, salaryData.RatePerHour, salaryData.Pay, salaryData.EmploymentTypeValue, salaryData.HasParking);

            var districtCoeff = await _fakeTaxService.GetChelyabinskDistrictCoeff();
            var personalIncomeTaxPercent = await _fakeTaxService.GetPersonalIncomeTaxPercent();
            var minimalSizeOfSalary = await _fakeTaxService.GetMinimalSizeOfSalary();

            calculatedSalaryData.CalculateMetrics(districtCoeff, minimalSizeOfSalary, personalIncomeTaxPercent);

            return calculatedSalaryData;
        }

        public async Task<FullEmployeeInformationDto> GetFullEmployeeInformation(CalculationServiceParameters request)
        {
            var employee = _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId).Result;
            var employeeFinancialMetrics = await CalculateEmployeeSalaryMetrics(request);

            return new FullEmployeeInformationDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.WorkEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram,
                Math.Round(employeeFinancialMetrics.Pay, 2),
                Math.Round(employeeFinancialMetrics.RatePerHour, 2),
                Math.Round(employeeFinancialMetrics.EmploymentType, 2),
                employeeFinancialMetrics.HasParking,
                Math.Round(employeeFinancialMetrics.HourlyCostFact, 2),
                Math.Round(employeeFinancialMetrics.HourlyCostHand, 2),
                Math.Round(employeeFinancialMetrics.Earnings, 2),
                Math.Round(employeeFinancialMetrics.Expenses, 2),
                Math.Round(employeeFinancialMetrics.Profit, 2),
                Math.Round(employeeFinancialMetrics.ProfitAbility, 2),
                Math.Round(employeeFinancialMetrics.GrossSalary, 2),
                Math.Round(employeeFinancialMetrics.Retainer, 2),
                Math.Round(employeeFinancialMetrics.NetSalary, 2));
        }
    }
}
