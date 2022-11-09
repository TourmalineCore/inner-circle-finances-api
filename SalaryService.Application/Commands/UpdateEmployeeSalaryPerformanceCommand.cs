using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeSalaryPerformanceCommand
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class UpdateEmployeeSalaryPerformanceCommandHandler
    {
        private readonly SalaryPerformanceRepository _salaryPerformanceRepository;

        public UpdateEmployeeSalaryPerformanceCommandHandler(SalaryPerformanceRepository salaryPerformanceRepository)
        {
            _salaryPerformanceRepository = salaryPerformanceRepository;
        }

        public void Handle(UpdateEmployeeSalaryPerformanceCommand request)
        {
            _salaryPerformanceRepository.UpdateEmployeeSalaryPerformance(new EmployeeSalaryPerformance(
                request.Id,
                request.EmployeeId,
                request.RatePerHour,
                request.FullSalary,
                request.EmploymentType,
                request.HasParking));
        }
    }
}
