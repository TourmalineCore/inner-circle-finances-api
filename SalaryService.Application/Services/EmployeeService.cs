using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceService _financeService;
        private readonly EmployeeRepository _employeeRepository;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

        public EmployeeService(FinanceService financeService,
            EmployeeRepository employeeRepository,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler)
        {
            _financeService = financeService;
            _employeeRepository = employeeRepository;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
        }

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var employee = await _createEmployeeCommandHandler.Handle(
            new CreateEmployeeCommand
                {
                    Name = parameters.Name,
                    Surname = parameters.Surname,
                    MiddleName = parameters.MiddleName,
                    WorkEmail = parameters.CorporateEmail,
                    PersonalEmail = parameters.PersonalEmail,
                    Phone = parameters.Phone,
                    Skype = parameters.Skype,
                    Telegram = parameters.Telegram
                }
            );

            var financeForPayrollId = await _financeService.CreateEmployeeFinanceForPayroll(employee.Id,
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.HasParking);

            var metricsId = await _financeService.CreateMetrics(employee.Id,
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.HasParking);

            await _employeeRepository.AddFinanceForPayrollAndMetrics(employee, financeForPayrollId, metricsId);
        }

        public async Task DeleteEmployee(long id)
        {
            await _deleteEmployeeCommandHandler.Handle(id);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.Handle(request);
        }
    }
}
