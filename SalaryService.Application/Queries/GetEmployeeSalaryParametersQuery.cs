using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeSalaryParametersQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetBasicSalaryParametersQueryHandler
    {
        private readonly BasicSalaryParametersRepository _salaryPerformanceRepository;

        public GetBasicSalaryParametersQueryHandler(BasicSalaryParametersRepository salaryPerformanceRepository)
        {
            _salaryPerformanceRepository = salaryPerformanceRepository;
        }
        public async Task<BasicSalaryParametersDto> Handle(GetEmployeeSalaryParametersQuery request)
        {
            var employee = await _salaryPerformanceRepository.GetByEmployeeIdAsync(request.EmployeeId);
            return new BasicSalaryParametersDto(
                employee.EmployeeId,
                employee.RatePerHour,
                employee.Pay,
                employee.EmploymentTypeValue, 
                employee.HasParking);
        }
    }
}
