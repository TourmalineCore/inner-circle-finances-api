using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceService _financeService;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly UpdateCEOCommandHandler _updateCEOCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly IClock _clock;

        public EmployeeService(FinanceService financeService,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateCEOCommandHandler updateCEOCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            IClock clock)
        {
            _financeService = financeService;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateCEOCommandHandler = updateCEOCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _clock = clock;
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
