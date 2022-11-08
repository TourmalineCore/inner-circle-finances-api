using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeSalaryParametersQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeSalaryParametersQueryHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public GetEmployeeSalaryParametersQueryHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<SalaryParametersDto> Handle(GetEmployeeSalaryParametersQuery request)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            return new SalaryParametersDto(
                employee.Id,
                employee.RatePerHour,
                employee.FullSalary,
                employee.EmploymentType);
        }
    }
}
