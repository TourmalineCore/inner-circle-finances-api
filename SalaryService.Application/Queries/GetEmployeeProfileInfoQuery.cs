using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeProfileInfoQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeProfileInfoQueryHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;

        public GetEmployeeProfileInfoQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
        }

        public async Task<EmployeeProfileDto> Handle(GetEmployeeProfileInfoQuery request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.EmployeeId);

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
