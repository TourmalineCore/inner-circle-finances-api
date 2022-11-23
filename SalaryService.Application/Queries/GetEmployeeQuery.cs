using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeeQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            return new EmployeeDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.CorporateEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram,
                employee.HireDate.ToString(),
                employee.EmployeeFinancialMetrics.NetSalary
                );
        }
    }
}
