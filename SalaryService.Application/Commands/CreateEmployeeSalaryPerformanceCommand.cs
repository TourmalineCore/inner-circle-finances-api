

using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeSalaryPerformanceCommand
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class CreateEmployeeSalaryPerformanceCommandHandler
    {
        private readonly SalaryPerformanceRepository _salaryPerformanceRepository;

        public CreateEmployeeSalaryPerformanceCommandHandler(SalaryPerformanceRepository salaryPerformanceRepository)
        {
            _salaryPerformanceRepository = salaryPerformanceRepository;
        }

        public async Task<long> Handle(CreateEmployeeSalaryPerformanceCommand request)
        {
            return await _salaryPerformanceRepository.CreateEmployeeSalaryPerformance(new EmployeeSalaryPerformance(
                request.Id,
                request.EmployeeId,
                request.RatePerHour,
                request.FullSalary,
                request.EmploymentType,
                request.HasParking));
        }
    }
}
