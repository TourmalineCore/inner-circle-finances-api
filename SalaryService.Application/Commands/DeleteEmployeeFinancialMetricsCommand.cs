using NodaTime;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinancialMetricsCommand
    {
        public long Id { get; set; }
    }

    public class DeleteEmployeeFinancialMetricsCommandHandler
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public DeleteEmployeeFinancialMetricsCommandHandler(EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository, IClock clock)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
        }

        public async Task Handle(DeleteEmployeeFinancialMetricsCommand request)
        {
            var metrics = await _employeeFinancialMetricsRepository.GetByIdAsync(request.Id);

            await _employeeFinancialMetricsRepository.RemoveAsync(metrics);
        }
    }
}
