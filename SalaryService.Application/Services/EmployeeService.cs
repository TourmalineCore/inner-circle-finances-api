using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceAnalyticService _financeAnalyticService;
        private readonly MailService _mailService;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly UpdateFinancesCommandHandler _updateFinancesCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly CalculatePreviewMetricsCommandHandler _calculatePreviewMetricsCommandHandler;
        private readonly CalculateTotalExpensesCommandHandler _calculateTotalExpensesCommandHandler;
        private readonly CalculateEstimatedFinancialEfficiencyCommandHandler _calculateEstimatedFinancialEfficiencyCommandHandler;

        public EmployeeService(FinanceAnalyticService financeAnalyticService,
            MailService mailService,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateFinancesCommandHandler updateFinancesCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            CalculatePreviewMetricsCommandHandler calculatePreviewMetricsCommandHandler, 
            CalculateTotalExpensesCommandHandler calculateTotalExpensesCommandHandler,
            CalculateEstimatedFinancialEfficiencyCommandHandler calculateEstimatedFinancialEfficiencyCommandHandler)
        {
            _financeAnalyticService = financeAnalyticService;
            _mailService = mailService;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateFinancesCommandHandler = updateFinancesCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _calculatePreviewMetricsCommandHandler = calculatePreviewMetricsCommandHandler;
            _calculateTotalExpensesCommandHandler = calculateTotalExpensesCommandHandler;
            _calculateEstimatedFinancialEfficiencyCommandHandler = calculateEstimatedFinancialEfficiencyCommandHandler;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(FinanceUpdatingParameters parameters)
        {
            var newMetrics = await _financeAnalyticService.CalculateMetrics(parameters.RatePerHour,
                parameters.Pay, parameters.EmploymentTypeValue, parameters.ParkingCostPerMonth);

            return await _calculatePreviewMetricsCommandHandler.Handle(parameters, newMetrics);
        }

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.ParkingCostPerMonth);

            var employee = _createEmployeeCommandHandler.Handle(parameters, metrics);
            _mailService.SendCredentials(employee.PersonalEmail, employee.CorporateEmail);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            _calculateTotalExpensesCommandHandler.Handle(totals);
            _calculateEstimatedFinancialEfficiencyCommandHandler.Handle(estimatedFinancialEfficiency);
        }

        public async Task DeleteEmployee(long id)
        {
            await _deleteEmployeeCommandHandler.Handle(id);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            _calculateTotalExpensesCommandHandler.Handle(totals);
            _calculateEstimatedFinancialEfficiencyCommandHandler.Handle(estimatedFinancialEfficiency);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.Handle(request);
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetrics(parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.ParkingCostPerMonth);

            await _updateFinancesCommandHandler.Handle(parameters, metrics);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            _calculateTotalExpensesCommandHandler.Handle(totals);
            _calculateEstimatedFinancialEfficiencyCommandHandler.Handle(estimatedFinancialEfficiency);
        }
    }
}
