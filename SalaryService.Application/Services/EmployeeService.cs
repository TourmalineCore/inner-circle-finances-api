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

        public EmployeeService(FinanceService financeService,
            EmployeeRepository employeeRepository,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateCEOCommandHandler updateCEOCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            IClock clock)
        {
            _financeService = financeService;
            _employeeRepository = employeeRepository;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateCEOCommandHandler = updateCEOCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _clock = clock;
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

            return CalculateDelta(preview, employee);
        }

        private MetricsPreviewDto CalculateDelta(MetricsPreviewDto preview, Employee employee)
        {
            preview.PayDelta = Math.Round(preview.Pay - employee.EmployeeFinancialMetrics.Pay, 2);
            preview.RatePerHourDelta = Math.Round(preview.RatePerHour - employee.EmployeeFinancialMetrics.RatePerHour, 2);
            preview.HourlyCostFactDelta = Math.Round(preview.HourlyCostFact - employee.EmployeeFinancialMetrics.HourlyCostFact, 2);
            preview.HourlyCostHandDelta = Math.Round(preview.HourlyCostHand - employee.EmployeeFinancialMetrics.HourlyCostHand, 2);
            preview.EarningsDelta = Math.Round(preview.Earnings - employee.EmployeeFinancialMetrics.Earnings, 2);
            preview.IncomeTaxContributionsDelta = Math.Round(preview.IncomeTaxContributions - employee.EmployeeFinancialMetrics.IncomeTaxContributions, 2);
            preview.PensionContributionsDelta = Math.Round(preview.PensionContributions - employee.EmployeeFinancialMetrics.PensionContributions, 2);
            preview.MedicalContributionsDelta = Math.Round(preview.MedicalContributions - employee.EmployeeFinancialMetrics.MedicalContributions, 2);
            preview.InjuriesContributionsDelta = Math.Round(preview.InjuriesContributions - employee.EmployeeFinancialMetrics.InjuriesContributions, 2);
            preview.ExpensesDelta = Math.Round(preview.Expenses - employee.EmployeeFinancialMetrics.Expenses, 2);
            preview.ProfitDelta = Math.Round(preview.Profit - employee.EmployeeFinancialMetrics.Profit, 2);
            preview.ProfitAbilityDelta = Math.Round(preview.ProfitAbility - employee.EmployeeFinancialMetrics.ProfitAbility, 2);
            preview.GrossSalaryDelta = Math.Round(preview.GrossSalary - employee.EmployeeFinancialMetrics.GrossSalary, 2);
            preview.RetainerDelta = Math.Round(preview.Retainer - employee.EmployeeFinancialMetrics.Retainer, 2);
            preview.NetSalaryDelta = Math.Round(preview.NetSalary - employee.EmployeeFinancialMetrics.NetSalary, 2);

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
