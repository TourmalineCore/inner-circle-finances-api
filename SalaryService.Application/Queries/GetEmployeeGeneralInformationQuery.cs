
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeGeneralInformationQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeGeneralInformationQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeeGeneralInformationQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeGeneralInformationDto> Handle(GetEmployeeGeneralInformationQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            return new EmployeeGeneralInformationDto(employee.Id,
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
