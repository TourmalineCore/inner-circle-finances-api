
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeInformationListQuery
    {
    }

    public class GetEmployeeInformationListQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeeInformationListQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeGeneralInformationDto>> Handle()
        {
            var employeesGeneralInformation = await _employeeRepository.GetAllAsync();

            return employeesGeneralInformation.Select(x => new EmployeeGeneralInformationDto(
                x.Id,
                x.Name,
                x.Surname,
                x.MiddleName,
                x.WorkEmail,
                x.PersonalEmail,
                x.Phone,
                x.Skype,
                x.Telegram
                )
            );
        }
    }
}
