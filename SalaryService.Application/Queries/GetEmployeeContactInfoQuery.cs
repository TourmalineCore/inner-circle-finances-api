
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeContactInfoQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeContactInfoQueryHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;

        public GetEmployeeContactInfoQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
        }
        public async Task<EmployeeContactInfoDto> Handle(GetEmployeeContactInfoQuery request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.EmployeeId);

            return new EmployeeContactInfoDto(employee.Id,
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
