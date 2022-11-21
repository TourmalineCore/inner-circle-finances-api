using NodaTime;
using SalaryService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinancialMetricsCommand
    {
        public long Id { get; set; }
    }

    public class DeleteEmployeeFinancialMetricsCommandHandler
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly IClock _clock;

        public DeleteEmployeeFinancialMetricsCommandHandler(EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository, IClock clock)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteEmployeeFinancialMetricsCommand request)
        {
            var metrics = await _employeeFinancialMetricsRepository.GetByIdAsync(request.Id);

            metrics.Delete(_clock.GetCurrentInstant());

            await _employeeFinancialMetricsRepository.UpdateAsync(metrics);
        }
    }
}
