using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetCEOQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetCEOQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetCEOQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<CEODto> Handle()
        {
            var employee = await _employeeRepository.GetCEOAsync();

            return new CEODto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.CorporateEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.GitHub,
                employee.GitLab
                );
        }
    }
}
