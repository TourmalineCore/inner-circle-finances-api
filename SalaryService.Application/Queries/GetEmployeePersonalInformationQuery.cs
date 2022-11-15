using SalaryService.DataAccess.Repositories;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeePersonalInformationQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeePersonalInformationQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public GetEmployeePersonalInformationQueryHandler(EmployeeRepository employeeRepository, EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task<EmployeePersonalInformationDto> Handle(GetEmployeePersonalInformationQuery request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            var basicSalaryParameters = await _employeeFinancialMetricsRepository.GetByEmployeeId(request.EmployeeId);

            return new EmployeePersonalInformationDto(employee.Id,
                employee.Name,
                employee.Surname,
                employee.MiddleName,
                employee.WorkEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.Skype,
                employee.Telegram,
                basicSalaryParameters.NetSalary);
        }
    }
}
