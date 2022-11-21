using NodaTime;
using SalaryService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinancialMetricsHistoryCommand
    {
        public long Id { get; set; }
    }

    public class DeleteEmployeeFinancialMetricsHistoryCommandHandler
    {
        private readonly MetricsHistoryRepository _metricsHistoryRepository;
        private readonly IClock _clock;

        public DeleteEmployeeFinancialMetricsHistoryCommandHandler(MetricsHistoryRepository metricsHistoryRepository,
            IClock clock)
        {
            _metricsHistoryRepository = metricsHistoryRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteEmployeeFinancialMetricsCommand request)
        {
            var metrics = await _metricsHistoryRepository.GetByIdAsync(request.Id);

            metrics.Delete(_clock.GetCurrentInstant());

            await _metricsHistoryRepository.UpdateAsync(metrics);
        }
    }
}
