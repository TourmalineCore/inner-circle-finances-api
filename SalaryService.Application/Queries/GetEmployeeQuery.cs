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

        public async Task<EmployeeProfileDto> Handle(GetEmployeeQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            return new EmployeeProfileDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.WorkEmail,
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
