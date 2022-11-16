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
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetEmployeeProfileInfoQueryHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<EmployeeProfileDto> Handle(GetEmployeeProfileInfoQuery request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.EmployeeId);
            var basicSalaryParameters = await _employeeFinancialMetricsRepository.GetByEmployeeId(request.EmployeeId);

            return new EmployeeProfileDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.WorkEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram,
                employee.EmploymentDate.ToString(),
                basicSalaryParameters.NetSalary
                );
        }
    }
}
