using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceAnalyticService _financeAnalyticService;
        private readonly RequestsService _requestsService;
        private readonly InnerCircleServiceUrl _urls;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly UpdateFinancesCommandHandler _updateFinancesCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly CalculatePreviewMetricsCommandHandler _calculatePreviewMetricsCommandHandler;
        private readonly CreateTotalExpensesCommandHandler _createTotalExpensesCommandHandler;
        private readonly CreateEstimatedFinancialEfficiencyCommandHandler _createEstimatedFinancialEfficiencyCommandHandler;

        public EmployeeService(FinanceAnalyticService financeAnalyticService,
            RequestsService requestsService,
            IOptions<InnerCircleServiceUrl> urls,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateFinancesCommandHandler updateFinancesCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            CalculatePreviewMetricsCommandHandler calculatePreviewMetricsCommandHandler, 
            CreateTotalExpensesCommandHandler createTotalExpensesCommandHandler,
            CreateEstimatedFinancialEfficiencyCommandHandler createEstimatedFinancialEfficiencyCommandHandler)
        {
            _financeAnalyticService = financeAnalyticService;
            _requestsService = requestsService;
            _urls = urls.Value;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateFinancesCommandHandler = updateFinancesCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _calculatePreviewMetricsCommandHandler = calculatePreviewMetricsCommandHandler;
            _createTotalExpensesCommandHandler = createTotalExpensesCommandHandler;
            _createEstimatedFinancialEfficiencyCommandHandler = createEstimatedFinancialEfficiencyCommandHandler;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(FinanceUpdatingParameters parameters)
        {
            var newMetrics = await _financeAnalyticService.CalculateMetrics(parameters.RatePerHour,
                parameters.Pay, parameters.EmploymentTypeValue, parameters.ParkingCostPerMonth);

            return await _calculatePreviewMetricsCommandHandler.HandleAsync(parameters, newMetrics);
        }

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.ParkingCostPerMonth);

            var employee = await _createEmployeeCommandHandler.HandleAsync(parameters, metrics);

            var securityCode = Guid.NewGuid();
            await _requestsService.SendPostRequest(_urls.AuthServiceUrl + "auth/register",
                new { Login = employee.CorporateEmail, Password = "", Code = securityCode });

            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }

        public async Task DeleteEmployee(long id)
        {
            await _deleteEmployeeCommandHandler.HandleAsync(id);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.HandleAsync(request);
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetrics(parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.ParkingCostPerMonth);

            await _updateFinancesCommandHandler.HandleAsync(parameters, metrics);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }
    }
}
