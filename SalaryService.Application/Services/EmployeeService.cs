using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceAnalyticService _financeAnalyticService;
        private readonly IInnerCircleHttpClient _innerCircleHttpClient;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly UpdateFinancesCommandHandler _updateFinancesCommandHandler;
        private readonly UpdateProfileCommandHandler _updateProfileCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly CreateTotalExpensesCommandHandler _createTotalExpensesCommandHandler;
        private readonly CreateEstimatedFinancialEfficiencyCommandHandler _createEstimatedFinancialEfficiencyCommandHandler;
        private readonly EmployeeUpdateCommandHandler _employeeUpdateCommandHandler; 

        public EmployeeService(FinanceAnalyticService financeAnalyticService,
            IInnerCircleHttpClient innerCircleHttpClient,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateFinancesCommandHandler updateFinancesCommandHandler,
            UpdateProfileCommandHandler updateProfileCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            CreateTotalExpensesCommandHandler createTotalExpensesCommandHandler,
            CreateEstimatedFinancialEfficiencyCommandHandler createEstimatedFinancialEfficiencyCommandHandler,
            EmployeeUpdateCommandHandler employeeUpdateCommandHandler)
        {
            _financeAnalyticService = financeAnalyticService;
            _innerCircleHttpClient = innerCircleHttpClient;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _updateFinancesCommandHandler = updateFinancesCommandHandler;
            _updateProfileCommandHandler = updateProfileCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _createTotalExpensesCommandHandler = createTotalExpensesCommandHandler;
            _createEstimatedFinancialEfficiencyCommandHandler = createEstimatedFinancialEfficiencyCommandHandler;
            _employeeUpdateCommandHandler = employeeUpdateCommandHandler;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(GetPreviewParameters parameters)
        {
            var newMetrics = await _financeAnalyticService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay, 
                parameters.EmploymentType, 
                parameters.ParkingCostPerMonth
            );

            return new MetricsPreviewDto(newMetrics.Pay, 
                newMetrics.RatePerHour, 
                newMetrics.EmploymentType, 
                newMetrics.Salary,
                newMetrics.ParkingCostPerMonth,
                newMetrics.AccountingPerMonth,
                newMetrics.HourlyCostFact,
                newMetrics.HourlyCostHand,
                newMetrics.Earnings,
                newMetrics.IncomeTaxContributions, 
                newMetrics.DistrictCoefficient,
                newMetrics.PensionContributions,
                newMetrics.MedicalContributions,
                newMetrics.SocialInsuranceContributions,
                newMetrics.InjuriesContributions,
                newMetrics.Expenses,
                newMetrics.Profit,
                newMetrics.ProfitAbility,
                newMetrics.GrossSalary,
                newMetrics.Prepayment,
                newMetrics.NetSalary);
        }

        public async Task CreateEmployeeAsync(EmployeeCreationParameters parameters)
        {
            await _createEmployeeCommandHandler.HandleAsync(parameters);
        }

        public async Task DeleteEmployee(long id)
        {
            await _deleteEmployeeCommandHandler.HandleAsync(id);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }

        public async Task UpdateEmployee(EmployeeUpdateParameters request)
        {
            await _employeeUpdateCommandHandler.HandleAsync(request);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters request)
        {
            await _updateEmployeeCommandHandler.HandleAsync(request);
        }

        public async Task UpdateProfileAsync(ProfileUpdatingParameters updatingParameters)
        {
            await _updateProfileCommandHandler.HandleAsync(updatingParameters);
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.ParkingCostPerMonth
            );

            await _updateFinancesCommandHandler.HandleAsync(parameters, metrics);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }
    }
}
