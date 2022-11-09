using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeByIdQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeByIdQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            return new EmployeeDto(
                employee.Id,
                employee.Name,
                employee.Surname,
                employee.WorkEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram);
        }
    }
}
