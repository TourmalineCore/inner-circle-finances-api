
using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeListQuery
    {
    }

    public class GetEmployeesQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeesQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle()
        {
            var employeesGeneralInformation = await _employeeRepository.GetAllAsync();

            return employeesGeneralInformation.Select(x => new EmployeeDto(
                    x.Id,
                    x.Name,
                    x.Surname,
                    x.MiddleName,
                    x.CorporateEmail,
                    x.PersonalEmail,
                    x.Phone,
                    x.GitHub,
                    x.GitLab,
                    x.EmployeeFinanceForPayroll.RatePerHour,
                    x.EmployeeFinanceForPayroll.Pay,
                    x.EmployeeFinanceForPayroll.EmploymentType,
                    x.EmployeeFinancialMetrics.NetSalary,
                    x.EmployeeFinancialMetrics.ParkingCostPerMonth
                )
            );
        }
    }
}
