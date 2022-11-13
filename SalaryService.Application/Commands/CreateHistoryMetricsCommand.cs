using Newtonsoft.Json;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class CreateHistoryMetricsCommand
    {
        public long EmployeeId { get; set; }
    }
    public class CreateHistoryMetricsCommandHandler
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly MetricsHistoryRepository _metricsHistoryRepository;


        public CreateHistoryMetricsCommandHandler( EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository,
            MetricsHistoryRepository metricsHistoryRepository)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _metricsHistoryRepository = metricsHistoryRepository;
        }
        public async Task<long> Handle(CreateHistoryMetricsCommand request)
        {
            var latestMetrics = await _employeeFinancialMetricsRepository.GetById(request.EmployeeId);
            var serializedMetrics = JsonConvert.SerializeObject(latestMetrics);
            var history = JsonConvert.DeserializeObject<EmployeeFinancialMetricsHistory>(serializedMetrics);
            return await _metricsHistoryRepository.CreateAsync(history);
        }
    }
}
