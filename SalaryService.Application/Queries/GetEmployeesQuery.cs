
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeesQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeesByIdQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeesByIdQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeContactDetailsDto> Handle(GetEmployeesQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            return new EmployeeContactDetailsDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.WorkEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram);
        }
    }
}
