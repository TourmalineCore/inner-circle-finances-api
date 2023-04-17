using FluentValidation;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Validators;

namespace SalaryService.Application.Services
{
    public class EmployeeService
    {
        private readonly FinanceAnalyticService _financeAnalyticService;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeInfoCommandHandler _updateEmployeeInfoCommandHandler;
        private readonly UpdateFinancesCommandHandler _updateFinancesCommandHandler;
        private readonly UpdateProfileCommandHandler _updateProfileCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly CreateTotalExpensesCommandHandler _createTotalExpensesCommandHandler;
        private readonly CreateEstimatedFinancialEfficiencyCommandHandler _createEstimatedFinancialEfficiencyCommandHandler;
        private readonly EmployeeUpdateParametersValidator _employeeUpdateParametersValidator;

        public EmployeeService(FinanceAnalyticService financeAnalyticService,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeInfoCommandHandler updateEmployeeInfoCommandHandler,
            UpdateFinancesCommandHandler updateFinancesCommandHandler,
            UpdateProfileCommandHandler updateProfileCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            CreateTotalExpensesCommandHandler createTotalExpensesCommandHandler,
            CreateEstimatedFinancialEfficiencyCommandHandler createEstimatedFinancialEfficiencyCommandHandler, 
            EmployeeUpdateParametersValidator employeeUpdateParametersValidator)
        {
            _financeAnalyticService = financeAnalyticService;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeInfoCommandHandler = updateEmployeeInfoCommandHandler;
            _updateFinancesCommandHandler = updateFinancesCommandHandler;
            _updateProfileCommandHandler = updateProfileCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _createTotalExpensesCommandHandler = createTotalExpensesCommandHandler;
            _createEstimatedFinancialEfficiencyCommandHandler = createEstimatedFinancialEfficiencyCommandHandler;
            _employeeUpdateParametersValidator = employeeUpdateParametersValidator;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(GetPreviewParameters parameters)
        {
            var newMetrics = await _financeAnalyticService.CalculateMetricsAsync(
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

        public async Task UpdateEmployeeAsync(EmployeeUpdateParameters request)
        {
            var validationResult = await _employeeUpdateParametersValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors[0].ErrorMessage);
            }

            await _updateEmployeeInfoCommandHandler.HandleAsync(request.GetEmployeeInfoUpdateParameters());
            await UpdateFinancesAsync(request.GetFinanceUpdatingParameters());
        }

        public async Task UpdateProfileAsync(ProfileUpdatingParameters updatingParameters)
        {
            await _updateProfileCommandHandler.HandleAsync(updatingParameters);
        }

        private async Task UpdateFinancesAsync(FinanceUpdatingParameters parameters)
        {
            var metrics = await _financeAnalyticService.CalculateMetricsAsync(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.ParkingCostPerMonth,
                parameters.EmployeeId
            );

            // TODO: #861m9k5f6: make all calculations in one transaction
            await _updateFinancesCommandHandler.HandleAsync(parameters, metrics);
            var totals = await _financeAnalyticService.CalculateTotalFinances();
            var estimatedFinancialEfficiency = await _financeAnalyticService.CalculateEstimatedFinancialEfficiency(totals.TotalExpense);
            await _createTotalExpensesCommandHandler.HandleAsync(totals);
            await _createEstimatedFinancialEfficiencyCommandHandler.HandleAsync(estimatedFinancialEfficiency);
        }
    }
}
