using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Commands;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public partial class EmployeeCreatingParameters
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public double EmploymentTypeValue => EmploymentType == EmploymentTypes.FullTime ? 1.0 : 0.5;

        public bool HasParking { get; set; }
    }

    public partial class EmployeeUpdatingParameters
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }
    }

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
            var employee = await CreateEmployeeProfile(parameters.Name,
                parameters.Surname,
                parameters.MiddleName,
                parameters.WorkEmail,
                parameters.PersonalEmail,
                parameters.Phone,
                parameters.Skype,
                parameters.Telegram);

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
            await _financeService.DeleteFinances(id);
            await _deleteEmployeeCommandHandler.Handle(id);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.Handle(request);
        }

        private Task<Employee> CreateEmployeeProfile(string name, 
            string surname, 
            string middleName, 
            string workEmail, 
            string personalEmail, 
            string phone, 
            string skype, 
            string telegram)
        {
            return _createEmployeeCommandHandler.Handle(
                new CreateEmployeeCommand
                {
                    Name = name,
                    Surname = surname,
                    MiddleName = middleName,
                    WorkEmail = workEmail,
                    PersonalEmail = personalEmail,
                    Phone = phone,
                    Skype = skype,
                    Telegram = telegram
                }
            );
        }
    }
}
