using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeCommand
    {

    }
    public class CreateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task Handle(Employee employee, EmployeeFinanceForPayroll financeForPayroll, EmployeeFinancialMetrics metrics)
        {
            await _employeeRepository.CreateAsync(employee, financeForPayroll, metrics);
        }
    }
}
