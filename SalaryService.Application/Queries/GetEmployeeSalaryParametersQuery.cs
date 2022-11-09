using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeSalaryParametersQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeSalaryPerformanceQueryHandler
    {
        private readonly SalaryPerformanceRepository _salaryPerformanceRepository;

        public GetEmployeeSalaryPerformanceQueryHandler(SalaryPerformanceRepository salaryPerformanceRepository)
        {
            _salaryPerformanceRepository = salaryPerformanceRepository;
        }
        public async Task<SalaryParametersDto> Handle(GetEmployeeSalaryParametersQuery request)
        {
            var employee = await _salaryPerformanceRepository.GetSalaryPerformanceByEmployeeIdAsync(request.EmployeeId);
            return new SalaryParametersDto(
                employee.EmployeeId,
                employee.RatePerHour,
                employee.FullSalary,
                employee.EmploymentType);
        }
    }
}
