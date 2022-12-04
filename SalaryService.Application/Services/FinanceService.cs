using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly CalculateTotalExpensesCommandHandler _calculateTotalExpensesCommandHandler;
        private readonly GetCoefficientsQueryHandler _getCoefficientsQueryHandler;
        private readonly IClock _clock;

        public FinanceAnalyticService(CalculateTotalExpensesCommandHandler calculateTotalExpensesCommandHandler,
            GetCoefficientsQueryHandler getCoefficientsQueryHandler,
            IClock clock)
        {
            _calculateTotalExpensesCommandHandler = calculateTotalExpensesCommandHandler;
            _getCoefficientsQueryHandler = getCoefficientsQueryHandler;
            _clock = clock;
        }

        public Task CalculateTotalFinances()
        {
            return _calculateTotalExpensesCommandHandler.Handle();
        }

        public async Task<EmployeeFinancialMetrics> CalculateMetrics(double ratePerHour,
            double pay,
            double employmentTypeValue,
            double parkingCostPerMonth)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(
                ratePerHour,
                pay,
                employmentTypeValue,
                parkingCostPerMonth);
            var coefficients = await _getCoefficientsQueryHandler.Handle();

            calculateMetrics.CalculateMetrics(coefficients.DistrictCoefficient,
                coefficients.MinimumWage,
                coefficients.IncomeTaxPercent,
                _clock.GetCurrentInstant());

            return calculateMetrics;
        }
    }
}
