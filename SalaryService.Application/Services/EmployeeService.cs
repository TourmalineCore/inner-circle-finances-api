using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceService _financeService;
        private readonly EmployeeRepository _employeeRepository;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly UpdateCEOCommandHandler _updateCEOCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly IClock _clock;
        private readonly CoefficientOptions _coefficientOptions;

        public EmployeeService(FinanceService financeService,
            EmployeeRepository employeeRepository,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateCEOCommandHandler updateCEOCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            IClock clock,
            IOptions<CoefficientOptions> coefficientOptions)
        {
            _financeService = financeService;
            _employeeRepository = employeeRepository;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateCEOCommandHandler = updateCEOCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _clock = clock;
            _coefficientOptions = coefficientOptions.Value;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(FinanceUpdatingParameters parameters)
        {
            var employee = await _employeeRepository.GetByIdAsync(parameters.EmployeeId);


            var calculatedMetrics = _financeService.CalculateMetrics(parameters.RatePerHour, 
                parameters.Pay, parameters.EmploymentTypeValue, parameters.HasParking);

            var preview = new MetricsPreviewDto(employee.Id,
                employee.Name + " " + employee.Surname + " " + employee.MiddleName,
                calculatedMetrics.Pay,
                calculatedMetrics.RatePerHour,
                calculatedMetrics.EmploymentType,
                calculatedMetrics.ParkingCostPerMonth,
                calculatedMetrics.HourlyCostFact,
                calculatedMetrics.HourlyCostHand,
                calculatedMetrics.Earnings,
                calculatedMetrics.IncomeTaxContributions,
                calculatedMetrics.PensionContributions,
                calculatedMetrics.MedicalContributions,
                calculatedMetrics.SocialInsuranceContributions,
                calculatedMetrics.InjuriesContributions,
                calculatedMetrics.Expenses,
                calculatedMetrics.Profit,
                calculatedMetrics.ProfitAbility,
                calculatedMetrics.GrossSalary,
                calculatedMetrics.Retainer,
                calculatedMetrics.NetSalary);

            preview.CalculateDelta(employee);
            return preview;
        }

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var employee = new Employee(parameters.Name, 
                parameters.Surname, 
                parameters.MiddleName,
                parameters.CorporateEmail, 
                parameters.PersonalEmail, 
                parameters.Phone, 
                parameters.GitHub,
                parameters.GitLab, 
                _clock.GetCurrentInstant());;

            var financeForPayroll = new EmployeeFinanceForPayroll(parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.HasParking);

            var metrics = _financeService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.HasParking);

            await _createEmployeeCommandHandler.Handle(employee, financeForPayroll, metrics);
        }

        public async Task DeleteEmployee(long id)
        {
            await _deleteEmployeeCommandHandler.Handle(id);
        }

        public async Task UpdateCEO(CEOUpdatingParameters request)
        {
            await _updateCEOCommandHandler.Handle(request);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.Handle(request);
        }
    }
}
