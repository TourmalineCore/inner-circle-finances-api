using Newtonsoft.Json;
using NodaTime;
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
        private readonly IClock _clock;


        public CreateHistoryMetricsCommandHandler( EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository,
            MetricsHistoryRepository metricsHistoryRepository,
            IClock clock)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _metricsHistoryRepository = metricsHistoryRepository;
            _clock = clock;
        }
        public async Task<long> Handle(CreateHistoryMetricsCommand request)
        {
            var latestMetrics = await _employeeFinancialMetricsRepository.GetById(request.EmployeeId);
            var history = new EmployeeFinancialMetricsHistory
            {
                EmployeeId = latestMetrics.EmployeeId,
                MetricsPeriod = new MetricsPeriod(latestMetrics.MetricsPeriod.StartedAtUtc, _clock.GetCurrentInstant()),
                Salary = latestMetrics.Salary,
                HourlyCostFact = latestMetrics.HourlyCostFact,
                HourlyCostHand = latestMetrics.HourlyCostHand,
                Earnings = latestMetrics.Earnings,
                Expenses = latestMetrics.Expenses,
                Profit = latestMetrics.Profit,
                ProfitAbility = latestMetrics.ProfitAbility,
                GrossSalary = latestMetrics.GrossSalary,
                NetSalary = latestMetrics.NetSalary,
                RatePerHour = latestMetrics.RatePerHour,
                Pay = latestMetrics.Pay,
                Retainer = latestMetrics.Retainer,
                EmploymentType = latestMetrics.EmploymentType,
                HasParking = latestMetrics.HasParking,
                ParkingCostPerMonth = latestMetrics.ParkingCostPerMonth,
                AccountingPerMonth = latestMetrics.AccountingPerMonth
            };
            return await _metricsHistoryRepository.CreateAsync(history);
        }
    }
}
