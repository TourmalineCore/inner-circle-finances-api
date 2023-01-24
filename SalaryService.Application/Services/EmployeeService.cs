using FluentValidation;
using Microsoft.Extensions.Logging;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        private IValidator<EmployeeCreatingParameters> _employeeCreatingParametersValidator;
        private IValidator<FinanceUpdatingParameters> _financeUpdatingParametersValidator;
        private IValidator<GetPreviewParameters> _getPreviewParametersValidator;

        public EmployeeService(FinanceAnalyticService financeAnalyticService,
            IInnerCircleHttpClient innerCircleHttpClient,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            UpdateFinancesCommandHandler updateFinancesCommandHandler,
            UpdateProfileCommandHandler updateProfileCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            CreateTotalExpensesCommandHandler createTotalExpensesCommandHandler,
            CreateEstimatedFinancialEfficiencyCommandHandler createEstimatedFinancialEfficiencyCommandHandler,
            IValidator<EmployeeCreatingParameters> employeeCreatingParametersValidator,
            IValidator<FinanceUpdatingParameters> financeUpdatingParametersValidator, 
            IValidator<GetPreviewParameters> getPreviewParametersValidator)
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
            _employeeCreatingParametersValidator = employeeCreatingParametersValidator;
            _financeUpdatingParametersValidator = financeUpdatingParametersValidator;
            _getPreviewParametersValidator = getPreviewParametersValidator;
        }

        public async Task<MetricsPreviewDto> GetPreviewMetrics(GetPreviewParameters parameters)
        {
            var resultValidation = await _getPreviewParametersValidator.ValidateAsync(parameters);

            if (!resultValidation.IsValid)
            {
                throw new Exception("Incorrect salary rate");
            }

            var newMetrics = await _financeAnalyticService.CalculateMetrics(parameters.RatePerHour,
                parameters.Pay, parameters.EmploymentTypeValue, parameters.ParkingCostPerMonth);

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

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var resultValidation = await _employeeCreatingParametersValidator.ValidateAsync(parameters);

            if (!resultValidation.IsValid)
            {
                throw new Exception("Incorrect salary rate");
            }

            var metrics = await _financeAnalyticService.CalculateMetrics(
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.ParkingCostPerMonth);

            var employee = await _createEmployeeCommandHandler.HandleAsync(parameters, metrics);

            await _innerCircleHttpClient.SendRequestToRegister(employee);

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

        public async Task UpdateProfile(ProfileUpdatingParameters request, long accountId)
        {
            await _updateProfileCommandHandler.HandleAsync(request, accountId);
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            var resultValidation = await _financeUpdatingParametersValidator.ValidateAsync(parameters);

            if (!resultValidation.IsValid)
            {
                throw new Exception("Incorrect salary rate");
            }

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
