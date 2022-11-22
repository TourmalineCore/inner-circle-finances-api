using NodaTime;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinancialMetricsCommand
    {
        
    }

    public class DeleteEmployeeFinancialMetricsCommandHandler
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public DeleteEmployeeFinancialMetricsCommandHandler(EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository, IClock clock)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task Handle(long employeeId)
        {
            var metrics = await _employeeFinancialMetricsRepository.GetByEmployeeId(employeeId);

            await _employeeFinancialMetricsRepository.RemoveAsync(metrics);
        }
    }
}
