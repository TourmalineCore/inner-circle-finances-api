
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeesListQuery
    {
    }

    public class GetEmployeesListQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeesListQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeContactDetailsDto>> Handle()
        {
            var employeesGeneralInformation = await _employeeRepository.GetAllAsync();

            return employeesGeneralInformation.Select(x => new EmployeeContactDetailsDto(
                x.Id,
                x.Name,
                x.Surname,
                x.MiddleName,
                x.CorporateEmail,
                x.PersonalEmail,
                x.Phone,
                x.GitHub,
                x.GitLab
                )
            );
        }
    }
}
