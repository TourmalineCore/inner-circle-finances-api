
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeContactInfoListQuery
    {
    }

    public class GetEmployeeContactInfoListQueryHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;

        public GetEmployeeContactInfoListQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
        }

        public async Task<IEnumerable<EmployeeContactInfoDto>> Handle()
        {
            var employeesGeneralInformation = await _employeeProfileInfoRepository.GetAllAsync();

            return employeesGeneralInformation.Select(x => new EmployeeContactInfoDto(
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
